namespace p4g64.sprint;

// Copid from the input library readme
[Flags]
public enum InputType: uint
{
    None = 0,
    Select = 0x0001,
    Start = 0x0008,
    Up = 0x0010,
    Right = 0x0020,
    Down = 0x0040,
    Left = 0x0080,
    L1 = 0x0400,
    R1 = 0x0800,
    Triangle = 0x1000,
    Circle = 0x2000,
    Cross = 0x4000,
    Square = 0x8000,
}