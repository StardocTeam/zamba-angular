package com.kendoui.spring.controllers.linecharts;

import java.text.ParseException;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.kendoui.spring.models.ChartDataRepository;

@Controller("dataviz-line_charts-visuals-controller")
@RequestMapping(value="/line-charts/")
public class VisualsController {
    @RequestMapping(value = "/visuals", method = RequestMethod.GET)
    public String index(Model model) throws ParseException {
        model.addAttribute("forecast", ChartDataRepository.ForecastData());
        
        return "/line-charts/visuals";
    }
}
