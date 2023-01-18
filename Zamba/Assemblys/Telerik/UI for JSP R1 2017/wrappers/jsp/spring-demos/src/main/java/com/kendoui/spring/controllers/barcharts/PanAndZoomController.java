package com.kendoui.spring.controllers.barcharts;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.kendoui.spring.models.ChartDataRepository;

@Controller("dataviz-bar_chart-pan_and_zoom-controller")
@RequestMapping(value="/bar-charts/")
public class PanAndZoomController {
    
    @RequestMapping(value = "/pan-and-zoom", method = RequestMethod.GET)
    public String index(Model model) {
        
        model.addAttribute("data", ChartDataRepository.PanAndZoomData());
     
        return "/bar-charts/pan-and-zoom";
    }
}
