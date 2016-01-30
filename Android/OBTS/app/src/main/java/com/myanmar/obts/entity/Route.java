package com.myanmar.obts.entity;

import android.os.Parcel;
import android.os.Parcelable;

import java.util.List;

/**
 * Created by minh on 1/12/2015.
 */
public class Route  implements Parcelable {
    String RouteId;
    String DepartureTime;
    String ArrivalTime;
    String Company;
    float Price;
    String CurrencyDesc;
    String BusType;

    private List<SeatItems> seats;
    public List<SeatItems> getseats(){return seats;}
    public void setseats(List<SeatItems> _seats){seats=_seats;}


    public String getRouteId(){return RouteId;}
    public String getDepartureTime(){return DepartureTime;}
    public String getArrivalTime(){return ArrivalTime;}
    public String getCompany(){return Company;}
    public float getPrice(){return Price;}
    public String getCurrencyDesc(){return CurrencyDesc;}
    public String getBusType(){return BusType;}

    public void setRouteId(String _routeId){RouteId=_routeId;}
    public void setDepartureTime(String _DepartureTime){DepartureTime=_DepartureTime;}
    public void setArrivalTime(String _ArrivalTime){ArrivalTime=_ArrivalTime;}
    public void setCompany(String _company){Company=_company;}
    public void setPrice(float _price){Price=_price;}
    public void setCurrencyDesc(String _CurrencyDesc){CurrencyDesc=_CurrencyDesc;}
    public void setBusType(String _busType){BusType=_busType;}

    @Override
    public int describeContents() {
        // TODO Auto-generated method stub
        return 0;
    }


    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(RouteId);
        dest.writeString(DepartureTime);
        dest.writeString(ArrivalTime);
        dest.writeString(Company);
        dest.writeFloat(Price);
        dest.writeString(CurrencyDesc);
        dest.writeString(BusType);
        dest.writeList(seats);
    }

    private Route(Parcel in){
        this.RouteId = in.readString();
        this.DepartureTime = in.readString();
        this.ArrivalTime = in.readString();
        this.Company = in.readString();
        this.Price=in.readFloat();
        this.CurrencyDesc = in.readString();
        this.BusType = in.readString();
        seats=in.readArrayList(SeatItems.class.getClassLoader());
    }

    public static final Parcelable.Creator<Route> CREATOR = new Parcelable.Creator<Route>() {

        @Override
        public Route createFromParcel(Parcel source) {
            return new Route(source);
        }

        @Override
        public Route[] newArray(int size) {
            return new Route[size];
        }
    };

    public Route(String _RouteId,String _DepartureTime,String _ArrivalTime,String _Company,float _Price,String _CurrencyDesc,String _BusType,List<SeatItems> _seats){
        this.RouteId = _RouteId;
        this.DepartureTime = _DepartureTime;
        this.ArrivalTime=_ArrivalTime;
        this.Company=_Company;
        this.Price=_Price;
        this.CurrencyDesc=_CurrencyDesc;
        this.BusType=_BusType;
        this.seats=_seats;
    }
}