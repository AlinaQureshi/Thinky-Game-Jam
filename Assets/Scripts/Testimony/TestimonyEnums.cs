using System;

[Serializable]
public class Testimony
{
    public string Key;
    public TestimonyItem Item;
    public TestimonyType CorrectAnswer;
    public TestimonyInfo SketchInfo;
    public TestimonyInfo NoteInfo;
}

[Serializable]
public class TestimonyInfo
{
    public TestimonyType Type;
    public string Description;
    public string Event;
}

public enum TestimonyType
{
    Sketch,
    Writing
}

public enum TestimonyItem
{
    GreenSquare,
    TwoSquares,

}