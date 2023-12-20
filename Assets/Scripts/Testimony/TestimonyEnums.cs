using System;

[Serializable]
public class Testimony
{
    public string Key;
    public TestimonyItem Item;
    public TestimonyType CorrectAnswer;
    public string TimelineEvent;
    public int TimelineOrder;
    public TestimonyInfo SketchInfo;
    public TestimonyInfo NoteInfo;
}

[Serializable]
public class TestimonyInfo
{
    public TestimonyType Type;
    public string Description;
}

public enum TestimonyType
{
    Sketch,
    Writing
}

public enum TestimonyItem
{
    Plants,
    DeskPapers,
    Window,
    Cat,
    Candlestick,
    Quill
}