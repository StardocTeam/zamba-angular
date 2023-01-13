package com.kendoui.spring.controllers.barcharts;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller("dataviz-bar_charts-visuals-controller")
@RequestMapping(value="/bar-charts/")
public class VisualsController {
    @RequestMapping(value = {"/visuals"}, method = RequestMethod.GET)
    public String index() {
        return "/bar-charts/visuals";
    }
}
