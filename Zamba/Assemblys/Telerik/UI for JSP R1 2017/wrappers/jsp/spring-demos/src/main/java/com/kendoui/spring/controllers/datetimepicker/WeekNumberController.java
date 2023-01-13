package com.kendoui.spring.controllers.datetimepicker;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("datetimepicker-week-column-controller")
@RequestMapping(value="/datetimepicker/")
public class WeekNumberController {
    
    @RequestMapping(value = {"/week-column"}, method = RequestMethod.GET)
    public String index() {       
        return "datetimepicker/week-column";
    }
}