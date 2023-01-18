using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Kendo.Mvc.Examples.Models
{
    public class AspNetMvcDescription : IFrameworkDescription
    {
        public string Name
        {
            get
            {
                return "ASP.NET MVC";
            }
        }

        public IEnumerable<ExampleFile> GetFiles(HttpServerUtilityBase server, string example, string section)
        {
            yield return new ExampleFile
            {
                Name = example + ".cshtml",
                Url = String.Format("~/Views/{0}/{1}.cshtml", section, example)
            };

            var path = server.MapPath("~/Controllers/");

            if (Directory.Exists(path))
            {
                var sections = Directory.GetDirectories(path);

                var directory = sections.FirstOrDefault(s =>
                {
                    var dir = Path.GetFileName(s.ToLower().Replace("_", "-"));

                    return dir.Equals(section) || dir.EndsWith(section + "s");
                });

                if (directory != null)
                {
                    var controllers = Directory.GetFiles(directory);

                    var controller = controllers.FirstOrDefault(c => Path.GetFileName(c).ToLower().Replace("_", "-") == example + "controller.cs");

                    if (controller != null)
                    {
                        yield return new ExampleFile
                        {
                            Name = Path.GetFileName(controller),
                            Url = "~/Controllers/" + Path.GetFileName(directory) + "/" + Path.GetFileName(controller)
                        };

                        var source = File.ReadAllText(controller);

                        if (source.Contains("productService"))
                        {
                            yield return new ExampleFile
                            {
                                Name = "ProductService.cs",
                                Url = "~/Models/ProductService.cs"
                            };
                        }

                        if (source.Contains("taskService"))
                        {
                            yield return new ExampleFile
                            {
                                Name = "SchedulerTaskService.cs",
                                Url = "~/Models/Scheduler/SchedulerTaskService.cs"
                            };
                        }

                        if (source.Contains("meetingService"))
                        {
                            yield return new ExampleFile
                            {
                                Name = "SchedulerMeetingService.cs",
                                Url = "~/Models/Scheduler/SchedulerMeetingService.cs"
                            };
                        }

                        if (source.Contains("TreeListController"))
                        {
                            yield return new ExampleFile
                            {
                                Name = "EmployeeDirectoryController.cs",
                                Url = "~/Controllers/TreeList/EmployeeDirectoryController.cs"
                            };
                        }
                    }
                }
            }
        }
    }
}
