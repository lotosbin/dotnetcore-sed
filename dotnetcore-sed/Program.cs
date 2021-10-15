using System;
using System.IO;
using System.Text.RegularExpressions;
using CommandLine;

namespace dotnetcore_sed {
    class Program {
        static void Main(string[] args) {
            Parser.Default.ParseArguments<Options>(args).WithParsed(o => {
                try {
                    if (!string.IsNullOrEmpty(o.Text)) {
                        //old mode
                        //dotnetcore-sed regex replacement text
                        //note InputFileName is first value
                        if (o.Verbose) {
                            Console.WriteLine($"replace regex={o.InputFileName} replacement={o.Replacement} text={o.Text}");
                        }

                        var result = Replace(o.InputFileName, o.Replacement, o.Text);
                        Console.WriteLine(result);
                    }
                    else {
                        //new mode
                        if (o.Verbose) {
                            Console.WriteLine($"replace -e {o.Expression} {o.InputFileName}");
                        }

                        var inputFilePath = Path.Combine(Environment.CurrentDirectory, o.InputFileName);
                        if (o.Verbose) {
                            Console.WriteLine($"input file: {inputFilePath}");
                        }

                        if (!File.Exists(inputFilePath)) {
                            throw new Exception("input file is not exit");
                        }

                        //  s/aaa/bbb/
                        var script = o.Expression.Split("/");
                        if (script[0] == "s") {
                            //read file
                            var text = File.ReadAllText(o.InputFileName);
                            var regex = script[1];
                            var replacement = script[2];
                            var result = Regex.Replace(text, regex, replacement, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(0.5));
                            if (o.InPlaceSuffix != null) {
                                var outputFilePath = Path.Combine(Environment.CurrentDirectory, o.InputFileName + (o.InPlaceSuffix ?? ""));
                                if (o.Verbose) {
                                    Console.WriteLine($"output file:{outputFilePath}");
                                }

                                File.WriteAllText(outputFilePath, result);
                            }
                            else {
                                Console.WriteLine(result);
                            }
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            });
        }

        private static string Replace(string pattern, string replacement, string text) {
            return Regex.Replace(text, pattern, replacement, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(0.5));
        }
    }
}