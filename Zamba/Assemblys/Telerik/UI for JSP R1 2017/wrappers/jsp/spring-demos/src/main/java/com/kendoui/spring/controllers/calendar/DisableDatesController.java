package com.kendoui.spring.controllers.calendar;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("calendar-disable-dates-controller")
@RequestMapping(value="/calendar/")
public class DisableDatesController {
    
    @RequestMapping(value = {"/disable-dates"}, method = RequestMethod.GET)
    public String index() {       
        return "calendar/disable-dates";
    }    
}