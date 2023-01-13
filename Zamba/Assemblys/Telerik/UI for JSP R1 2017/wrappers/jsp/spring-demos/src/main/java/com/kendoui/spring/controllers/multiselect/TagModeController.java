
package com.kendoui.spring.controllers.multiselect;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.kendoui.spring.models.DropDownListItem;

@Controller("multiselect-tagmode-controller")
@RequestMapping(value="/multiselect/")
public class TagModeController {

    @RequestMapping(value = {"/tag-mode"}, method = RequestMethod.GET)
    public String index(Model model) {

        model.addAttribute("attendees", new String[] {
            "Steven White",
            "Nancy King",
            "Anne King",
            "Nancy Davolio",
            "Robert Davolio",
            "Michael Leverling",
            "Andrew Callahan",
            "Michael Suyama",
            "Anne King",
            "Laura Peacock",
            "Robert Fuller",
            "Janet White",
            "Nancy Leverling",
            "Robert Buchanan",
            "Andrew Fuller",
            "Anne Davolio",
            "Andrew Suyama",
            "Nige Buchanan",
            "Laura Fuller"
        });

        return "multiselect/tag-mode";
    }
}
