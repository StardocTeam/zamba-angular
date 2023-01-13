package com.kendoui.spring.models;

public class Forecast {
    private String weather;
    private int temperature;
    private String day;
    
    public Forecast(String weather, int temperature, String day)
    {
        this.weather = weather;
        this.temperature = temperature;
        this.day = day;
    }
    
    public String getWeather() {
        return weather;
    }
    
    public void setWeather(String value) {
        weather = value;
    }
    
    public String getDay() {
        return day;
    }
    public void setDay(String value) {
        day = value;
    }
    
    public int getTemperature() {
        return temperature;
    }
    
    public void setTemperature(int value) {
        temperature = value;
    }
}
