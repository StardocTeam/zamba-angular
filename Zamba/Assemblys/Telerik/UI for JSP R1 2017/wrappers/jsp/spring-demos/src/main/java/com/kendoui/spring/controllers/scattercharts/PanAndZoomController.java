package com.kendoui.spring.controllers.scattercharts;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.kendoui.spring.models.ChartDataRepository;

@Controller("dataviz-scatter_chart-pan_and_zoom-controller")
@RequestMapping(value="/scatter-charts/")
public class PanAndZoomController {
    @RequestMapping(value = "/pan-and-zoom", method = RequestMethod.GET)
    public String index(Model model) {
        model.addAttribute("data", ChartDataRepository.SineInterval());
     
        return "/scatter-charts/pan-and-zoom";
    }
}
