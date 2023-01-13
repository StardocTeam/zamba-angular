using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.UI;

namespace Kendo.Mvc.Examples.Models
{
    public static class PanelBarRepository
    {
        private static List<PanelBarItemViewModel> projectData;

        static PanelBarRepository()
        {
            projectData = new List<PanelBarItemViewModel>();
            projectData.Add(new PanelBarItemViewModel
            {
                id = "1",
                text = "My Documents",
                expanded = true,
                hasChildren = true,
                spriteCssClass = "rootfolder",
                items = new List<PanelBarItemViewModel>
                       {
                           new PanelBarItemViewModel
                           {
                                id = "2",
                                text = "Kendo UI Project",
                                expanded  = true,
                                spriteCssClass = "folder",
                                hasChildren = true,
                                items = new List<PanelBarItemViewModel>
                                {
                                    new PanelBarItemViewModel
                                    {
                                            id = "3",
                                            text ="about.html",
                                            spriteCssClass = "html"
                                    },
                                    new PanelBarItemViewModel
                                    {
                                            id = "4",
                                            text ="index.html",
                                            spriteCssClass = "html"
                                    },
                                    new PanelBarItemViewModel
                                    {
                                            id = "5",
                                            text ="logo.png",
                                            spriteCssClass = "image"
                                    }
                                }
                           },
                           new PanelBarItemViewModel
                           {
                                id = "6",
                                text = "New Web Site",
                                expanded  = true,
                                spriteCssClass = "folder",
                                hasChildren = true,
                                items = new List<PanelBarItemViewModel>
                                {
                                    new PanelBarItemViewModel
                                    {
                                            id = "7",
                                            text ="mockup.jpg",
                                            spriteCssClass = "image"
                                    },
                                    new PanelBarItemViewModel
                                    {
                                            id = "8",
                                            text ="Research.pdf",
                                            spriteCssClass = "pdf"
                                    }
                                }
                           },
                           new PanelBarItemViewModel
                           {
                                id = "9",
                                text = "Reports",
                                expanded  = true,
                                spriteCssClass = "folder",
                                hasChildren = true,
                                items = new List<PanelBarItemViewModel>
                                {
                                    new PanelBarItemViewModel
                                    {
                                            id = "10",
                                            text ="February.pdf",
                                            spriteCssClass = "pdf"
                                    },
                                    new PanelBarItemViewModel
                                    {
                                            id = "11",
                                            text ="March.pdf",
                                            spriteCssClass = "pdf"
                                    },
                                        new PanelBarItemViewModel
                                    {
                                            id = "12",
                                            text ="April.pdf",
                                            spriteCssClass = "pdf"
                                    }
                                }
                           }
                       }
            });

        }

        public static List<PanelBarItemViewModel> GetProjectData()
        {
            return projectData;
        }

        public static IEnumerable<PanelBarItemViewModel> GetChildren(string id)
        {
            Queue<PanelBarItemViewModel> items = new Queue<PanelBarItemViewModel>(projectData);

            while (items.Count > 0)
            {
                var current = items.Dequeue();
                if (current.id == id)
                {
                    return current.items.Select(o => new PanelBarItemViewModel
                    {
                        id = o.id,
                        text = o.text,
                        expanded = o.expanded,
                        hasChildren = o.hasChildren,
                        imageUrl = o.imageUrl,
                        spriteCssClass = o.spriteCssClass
                    });
                }

                if (current.hasChildren)
                {
                    foreach (var item in current.items)
                    {
                        items.Enqueue(item);
                    }
                }
            }

            return new List<PanelBarItemViewModel>();
        }
    }
}