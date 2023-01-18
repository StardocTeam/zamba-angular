package com.kendoui.spring.controllers.datepicker;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("datepicker-week-column-controller")
@RequestMapping(value="/datepicker/")
public class WeekNumberController {
    
    @RequestMapping(value = {"/week-column"}, method = RequestMethod.GET)
    public String index() {       
        return "datepicker/week-column";
    }
}