package com.kendoui.spring.controllers.gantt;

import java.util.Locale;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("gantt-right-to-left-support-controller")
@RequestMapping(value="/gantt/")
public class RightToLeftSupportController{
    @RequestMapping(value = {"/right-to-left-support"}, method = RequestMethod.GET)
    public String api(Locale locale, Model model) {        
        return "gantt/right-to-left-support";
    }
}