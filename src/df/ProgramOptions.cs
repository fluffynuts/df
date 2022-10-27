using PeanutButter.EasyArgs.Attributes;

namespace df
{
    public class ProgramOptions
    {
        [Default(true)]
        public bool HumanReadable { get; set; }
        [LongName("si")]
        [ShortName('H')]
        public bool SI { get; set; }
    }
}