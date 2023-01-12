package com.kendoui.spring.controllers.pivotgrid;

import java.io.IOException;

import javax.servlet.http.HttpServletResponse;
import javax.xml.bind.DatatypeConverter;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.kendoui.spring.models.DataSourceRequest;
import com.kendoui.spring.models.DataSourceResult;


@Controller("pivotgrid-excel-export-controller")
@RequestMapping(value="/pivotgrid/")
public class ExcelExportController {
        
    @RequestMapping(value = "/excel-export", method = RequestMethod.GET)
    public String index() {
        return "pivotgrid/excel-export";
    }

    @RequestMapping(value = "/excel-export/save", method = RequestMethod.POST)
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

