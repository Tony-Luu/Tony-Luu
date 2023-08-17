using UnityEngine;

public static class DetectOverlappingRectTransforms
{
    public static Rect ReturnConvertedRectLocalPositionToRectWorldPosition(RectTransform RectLocalTransform)
    {
        Rect ConvertedRect = new Rect();

        if (RectLocalTransform != null)
        {
            //Obtain the rect from the rect transform
            Rect RectLocal = RectLocalTransform.rect;

            //Set an array of vector3 to store the world corners of the scene
            Vector3[] WorldCorners = new Vector3[4];

            //Obtain the world corners
            RectLocalTransform.GetWorldCorners(WorldCorners);

            //Obtain the world center position by adding the bottom-left corner position with the top-right corner position then half it by 2
            Vector3 WorldCenterPosition = (WorldCorners[0] + WorldCorners[2]) / 2f;

            //Scale the size of the rectangle's local size with the rect transform's lossy scale
            RectLocal.size = Vector2.Scale(RectLocal.size, RectLocalTransform.lossyScale);

            //Create a new rect with the world center position and the scaled size
            ConvertedRect = new Rect(WorldCenterPosition, RectLocal.size);

            //Convert the center of the rect from local to world position
            ConvertedRect.center = RectLocalTransform.TransformPoint(ConvertedRect.center);
        }
        //Return the new rect
        return ConvertedRect;
    }

    //Returns either true or false if the two rect transforms are overlapping with each other
    public static bool IsFirstRectTransformOverlappingWithSecondRectTransform(RectTransform FirstRect, RectTransform SecondRect)
    {
        Rect RectOne = ReturnConvertedRectLocalPositionToRectWorldPosition(FirstRect);
        Rect RectTwo = ReturnConvertedRectLocalPositionToRectWorldPosition(SecondRect);
        return RectOne.Overlaps(RectTwo, true);
    }

}
