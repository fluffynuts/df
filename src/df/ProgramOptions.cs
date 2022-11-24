namespace df
{
    public class ProgramOptions
    {
        public bool HumanReadable { get; set; }
        public bool StandardInternationalUnits { get; set; }

        public static ProgramOptions From(string[] args)
        {
            var result = new ProgramOptions();
            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "-h":
                    case "--human-readable":
                        result.HumanReadable = true;
                        break;
                    case "-H":
                    case "--si":
                        result.StandardInternationalUnits = true;
                        break;
                    case "--help":
                        ShowHelp();
                        Environment.Exit(0);
                        break;
                }
            }

            return result;
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@$"
df [options]
  -h, --human-readable  print sizes in powers of 1024 (e.g., 1023M)
  -H, --si              print sizes in powers of 1000 (e.g., 1.1G)
".Trim());
        }
    }
}