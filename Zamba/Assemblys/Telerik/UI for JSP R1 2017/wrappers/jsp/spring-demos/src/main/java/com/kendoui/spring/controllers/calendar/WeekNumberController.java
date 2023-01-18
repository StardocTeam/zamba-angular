package com.kendoui.spring.controllers.calendar;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("calendar-week-column-controller")
@RequestMapping(value="/calendar/")
public class WeekNumberController {
    
    @RequestMapping(value = {"/week-column"}, method = RequestMethod.GET)
    public String index() {       
        return "calendar/week-column";
    }
}