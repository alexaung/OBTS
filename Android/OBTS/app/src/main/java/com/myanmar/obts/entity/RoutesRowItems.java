package com.myanmar.obts.entity;

import android.widget.Space;

import java.util.List;

/**
 * Created by minh on 25/11/2015.
 */
public class RoutesRowItems {
    private String RouteId;
    private String DepartureTime;
    private String ArrivalTime;
    private String RouteFare;
    private String Company;
    private String RouteDuration;
    private String BusType;
    private String TotalSeats;
    private String CurrencyDesc;

    private String BusId;
    public String getBusId(){return BusId;}
    public void setBusId(String _BusId){BusId=_BusId;}

    private short Brand;
    public short getBrand(){return Brand;}
    public void setBrand(short _Brand){Brand=_Brand;}

    private String BrandDesc;
    public String getBrandDesc(){return BrandDesc;}
    public void setBrandDesc(String _BrandDesc){BrandDesc=_BrandDesc;}

    private String BusTypeDesc;
    public String getBusTypeDesc(){return BusTypeDesc;}
    public void setBusTypeDesc(String _BusTypeDesc){BusTypeDesc=_BusTypeDesc;}

    private String RegistrationNo;
    public String getRegistrationNo(){return RegistrationNo;}
    public void setRegistrationNo(String _RegistrationNo){RegistrationNo=_RegistrationNo;}

    private String VechiclePhoneNo;
    public String getVechiclePhoneNo(){return VechiclePhoneNo;}
    public void setVechiclePhoneNo(String _VechiclePhoneNo){VechiclePhoneNo=_VechiclePhoneNo;}

    private String Source_CityId;
    public String getSource_CityId(){return Source_CityId;}
    public void setSource_CityId(String _Source_CityId){Source_CityId=_Source_CityId;}

    private String SourceCity;
    public String getSourceCity(){return SourceCity;}
    public void setSourceCity(String _SourceCity){SourceCity=_SourceCity;}

    private String Destination_CityId;
    public String getDestination_CityId(){return Destination_CityId;}
    public void setDestination_CityId(String _Destination_CityId){Destination_CityId=_Destination_CityId;}

    private String DestinationCity;
    public String getDestinationCity(){return DestinationCity;}
    public void setDestinationCity(String _DestinationCity){DestinationCity=_DestinationCity;}

    private boolean Recurrsive;
    public boolean getRecurrsive(){return Recurrsive;}
    public void setRecurrsive(boolean _Recurrsive){Recurrsive=_Recurrsive;}


    private String RouteDate;
    public String getRouteDate(){return RouteDate;}
    public void setRouteDate(String _RouteDate){RouteDate=_RouteDate;}

    public List<SeatItems> seats;
    public List<SeatItems> getseats(){return seats;}
    public void setseats(List<SeatItems> _seats){seats=_seats;}

    public String getCurrencyDesc(){return CurrencyDesc;}

    public void setCurrencyDesc(String _CurrencyDesc){CurrencyDesc=_CurrencyDesc;}

    public String getRouteId(){return RouteId;}

    public void setRouteId(String  _RouteId){RouteId=_RouteId;}

    public String getDepartureTime() {
        String _DepartureTime="";
        String[] D=DepartureTime.split(":");
        float depart=Float.valueOf(D[0]+"."+D[1]);

        if(depart<12)
            _DepartureTime=D[0]+":"+D[1]+" AM";
        else if(depart>=12) {
            depart=12-(24-depart);
            D=String.valueOf(depart).split(".");
            if(D.length==0)
                _DepartureTime =String.valueOf((int)depart) + ":00 PM";
            else
                _DepartureTime = D[0] + ":" + D[1] + " PM";
        }
        return _DepartureTime;
    }

    public void setDepartureTime(String _DepartureTime) {
        this.DepartureTime = _DepartureTime;
    }

    public String getArrivalTime() {
        String _ArrivalTime="";
        String[] D=ArrivalTime.split(":");
        float depart=Float.valueOf(D[0]+"."+D[1]);

        if(depart<12)
            _ArrivalTime=D[0]+":"+D[1]+" AM";
        else if(depart>=12) {
            depart=12-(24-depart);
            D=String.valueOf(depart).split(".");
            if(D.length==0)
                _ArrivalTime = String.valueOf((int)depart) + ":00 PM";
            else
                _ArrivalTime = D[0] + ":" + D[1] + " PM";
        }
        return _ArrivalTime;
    }

    public void setArrivalTime(String _ArrivalTime) {
        this.ArrivalTime = _ArrivalTime;
    }

    public String getRouteFare() {
        return RouteFare;
    }

    public void setRouteFare(String _RouteFare) {
        this.RouteFare = _RouteFare;
    }

    public String getCompany() {
        return Company;
    }

    public void setCompany(String _Company) {
        this.Company = _Company;
    }

    public String getRouteDuration() {
        String[] D=DepartureTime.split(":");
        float Depart=0;
        Depart = Float.valueOf(D[0]+"."+D[1]);

        String[] A=ArrivalTime.split(":");
        float Arrival=0;
        Arrival = Float.valueOf(A[0]+"."+A[1]);

        float Dur=0;
        if(Arrival<Depart)
            Dur=(24-Depart)+Arrival;
        else
            Dur=Arrival-Depart;
        String[] aa=String.valueOf(Dur).split(".");
        if(aa.length==0) return String.valueOf((int)Dur)+" hrs";

        return  aa[0]+":"+aa[1]+" hrs";
    }

    /*public void setRouteDuration(String _RouteDuration) {
        this.RouteDuration = _RouteDuration;
    }*/

    public String getBusType() {
        return BusType;
    }

    public void setBusType(String _BusType) {
        this.BusType = _BusType;
    }

    public String getTotalSeats() {
        int count=0;
        for(int i=0;i<seats.size();i++)
        {
            if(seats.get(i).getState()==SeatState.Available.getValue())
                count++;
        }
        return String.valueOf(count)+ " seats";
    }

}
