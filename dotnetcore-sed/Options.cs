using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace dotnetcore_sed {
    public class Options {
        [Option('e', "expression", HelpText = "add the script to the commands to be executed")]
        public string Expression { get; set; }

        [Option('i', "in-place", HelpText = "edit files in place (makes backup if SUFFIX supplied)")]
        public string InPlaceSuffix { get; set; }

        [Value(0)] public string InputFileName { get; set; }
        [Value(1)] public string Replacement { get; set; }
        [Value(2)] public string Text { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Usage(ApplicationAlias = "dotnetcore-sed")]
        // ReSharper disable once UnusedMember.Global
        public static IEnumerable<Example> Examples => new List<Example>() {
            new("sed replace aaa with bbb in file sample.text ", new Options {
                InPlaceSuffix = "",
                Expression = "s/aaa/bbb/",
                InputFileName = "sample.txt"
            }),
            new("sed replace aaa with bbb in textaaatext ", new Options {
                InputFileName = "aaa",
                Replacement = "bbb",
                Text = "textaaatext"
            })
        };
    }
}