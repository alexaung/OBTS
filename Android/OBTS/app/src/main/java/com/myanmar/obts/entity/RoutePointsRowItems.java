package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
public class RoutePointsRowItems {
    private String RouteId;
    private String BoardingTime;
    private String DroppingTime;
    private String BoardingPoint;
    private String DroppingPoint;

    public String getRouteId(){return RouteId;}
    public void setRouteId(String _RouteId){RouteId=_RouteId;}

    public String getBoardingTime(){return BoardingTime;}
    public void setBoardingTime(String _BoardingTime){BoardingTime=_BoardingTime;}

    public String getDroppingTime(){return DroppingTime;}
    public void setDroppingTime(String _DroppingTime){DroppingTime=_DroppingTime;}

    public String getBoardingPoint(){return BoardingPoint;}
    public void setBoardingPoint(String _BoardingPoint){BoardingPoint=_BoardingPoint;}

    public String getDroppingPoint(){return DroppingPoint;}
    public void setDroppingPoint(String _DroppingPoint){DroppingPoint=_DroppingPoint;}
}
