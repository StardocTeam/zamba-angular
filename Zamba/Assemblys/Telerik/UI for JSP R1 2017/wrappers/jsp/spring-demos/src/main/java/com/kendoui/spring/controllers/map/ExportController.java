package com.kendoui.spring.controllers.map;

import java.io.IOException;

import javax.servlet.http.HttpServletResponse;
import javax.xml.bind.DatatypeConverter;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller("map-export-controller")
@RequestMapping(value="/map/")
public class ExportController {
    @RequestMapping(value = {"/export"}, method = RequestMethod.GET)
    public String index() {
        return "map/export";
    }

    @RequestMapping(value = "/export/save", method = RequestMethod.POST)
    public @ResponseBody
    void save(String fileName, String base64, String contentType,
            HttpServletResponse response) throws IOException {

        response.setHeader("Content-Disposition", "attachment;filename=" + fileName);
        response.setContentType(contentType);

        byte[] data = DatatypeConverter.parseBase64Binary(base64);

        response.setContentLength(data.length);
        response.getOutputStream().write(data);
        response.flushBuffer();
    }
}

