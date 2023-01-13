package com.kendoui.spring.controllers.spreadsheet;

import java.io.IOException;

import javax.servlet.http.HttpServletResponse;
import javax.xml.bind.DatatypeConverter;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;


@Controller("spreadsheet-home-controller")
@RequestMapping(value="/spreadsheet/")
public class IndexController {
    @RequestMapping(value = {"/", "/index"}, method = RequestMethod.GET)
    public String index() {
        return "spreadsheet/index";
    }

    @RequestMapping(value = "/index/save", method = RequestMethod.POST)
    public @ResponseBody
    void save(String fileName, String base64, String contentType,
            HttpServletResponse response) throws IOException {

        response.setHeader("Content-Disposition", "attachment;filename="
                + fileName);
        response.setContentType(contentType);

        byte[] data = DatatypeConverter.parseBase64Binary(base64);

        response.setContentLength(data.length);
        response.getOutputStream().write(data);
        response.flushBuffer();
    }
}

