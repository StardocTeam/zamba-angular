package com.kendoui.spring.controllers.gantt;

import java.util.Locale;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("gantt-column-resizing-controller")
@RequestMapping(value="/gantt/")
public class ColumnResizingController {
    @RequestMapping(value = {"/column-resizing"}, method = RequestMethod.GET)
    public String events(Locale locale, Model model) {        
        return "gantt/column-resizing";
    }
}