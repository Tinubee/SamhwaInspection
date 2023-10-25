using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamhwaInspection.Schemas
{
    public enum SearchMode
    {
        [Description("BigOne")]
        BigOne = 0,

        [Description("All")]
        WhiteBlob = 1,
    }
    public class 검사도구모음
    {
        public Scalar RED;
        public Scalar GREEN;

        public 검사도구모음()
        {
            this.RED = new Scalar(0, 0, 255);
            this.GREEN = new Scalar(0, 255, 0);
        }

        public List<Rect> FindBlobs2(Mat srcImage, Rect region, double threshold, ThresholdTypes thresholdType = ThresholdTypes.Binary, SearchMode searchMode = SearchMode.BigOne, Int32 minArea = 300000, Int32 maxArea = 500000)
        {
            // 이미지에서 Region을 잘라냅니다.
            Mat croppedImage = srcImage.SubMat(region);

            // 이미지를 흑백으로 변환합니다.
            Mat grayImage = new Mat();
            if (croppedImage.Channels() != 1)
            {
                Cv2.CvtColor(croppedImage, grayImage, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayImage = croppedImage;
            }

            // 이진화를 적용합니다.
            Mat binaryImage = new Mat();
            Cv2.Threshold(grayImage, binaryImage, threshold, 255, thresholdType);

            // Blob을 찾기 위해 외곽선을 검출합니다.
            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binaryImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // Blob 영역을 저장할 리스트를 생성합니다.
            List<Rect> blobs = new List<Rect>();
            
            // 외곽선을 순회하며 Blob 영역을 추출합니다.
            foreach (var contour in contours)
            {
                Rect blob = Cv2.BoundingRect(contour);
                Int32 Area = blob.Width * blob.Height;
                //지정한 범위 내에 있는 blob만 찾기
                if ((Area > maxArea) || (Area < minArea)) 
                {
                    continue;
                }
                
                // Region 좌표를 다시 추가합니다.
                blob.X += region.X;
                blob.Y += region.Y;
                blobs.Add(blob);
            }

            //Y축에 따라 오름차순으로 정렬하여 리턴
            blobs.Sort((rect1, rect2) => rect1.Y.CompareTo(rect2.Y));

            Debug.WriteLine("Sort완료");
            return blobs;
        }

        public List<Rect> FindBlobs(Mat srcImage, Rect region, double threshold, ThresholdTypes thresholdType = ThresholdTypes.Binary, SearchMode searchMode = SearchMode.BigOne)
        {
            // 이미지에서 Region을 잘라냅니다.
            Mat croppedImage = srcImage.SubMat(region);

            // 이미지를 흑백으로 변환합니다.
            Mat grayImage = new Mat();
            if (croppedImage.Channels() != 1)
            {
                Cv2.CvtColor(croppedImage, grayImage, ColorConversionCodes.BGR2GRAY);
            }
            else
            {
                grayImage = croppedImage;
            }

            // 이진화를 적용합니다.
            Mat binaryImage = new Mat();
            Cv2.Threshold(grayImage, binaryImage, threshold, 255, thresholdType);

            // Blob을 찾기 위해 외곽선을 검출합니다.
            Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binaryImage, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // Blob 영역을 저장할 리스트를 생성합니다.
            List<Rect> blobs = new List<Rect>();

            // 외곽선을 순회하며 Blob 영역을 추출합니다.
            foreach (var contour in contours)
            {
                Rect blob = Cv2.BoundingRect(contour);
                // Region 좌표를 다시 추가합니다.
                blob.X += region.X;
                blob.Y += region.Y;
                blobs.Add(blob);
            }

            return blobs;
        }

        public Rect FindLargestBlob(List<Rect> blobs, Rect rect)
        {
            if (blobs.Count == 0)
            {
                return rect;
            }

            // 가장 큰 Blob을 찾습니다.
            Rect largestBlob = blobs[0];
            foreach (var blob in blobs)
            {
                if (blob.Width * blob.Height > largestBlob.Width * largestBlob.Height)
                {
                    largestBlob = blob;
                }
            }

            return largestBlob;
        }

        public void DrawBlobs(Mat srcImage, List<Rect> blobs, Scalar color, int thickness)
        {
            foreach (var blob in blobs)
            {
                if (srcImage.Channels() == 1)
                {
                    Cv2.CvtColor(srcImage, srcImage, ColorConversionCodes.GRAY2BGR);
                }
                Cv2.Rectangle(srcImage, blob, color, thickness);
            }
        }
        public void DrawLargestBlob(Mat srcImage, Rect largestBlob, Scalar color, int thickness)
        {
            Cv2.Rectangle(srcImage, largestBlob, color, thickness);
        }
    }
}
