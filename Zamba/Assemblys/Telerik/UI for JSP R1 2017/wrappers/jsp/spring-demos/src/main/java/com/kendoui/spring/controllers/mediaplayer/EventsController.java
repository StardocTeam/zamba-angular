package com.kendoui.spring.controllers.mediaplayer;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("mediaplayer-events-controller")
@RequestMapping(value="/mediaplayer/")
public class EventsController {
    @RequestMapping(value = {"/events"}, method = RequestMethod.GET)
    public String index() {
        return "mediaplayer/events";
    }
}

