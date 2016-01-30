package com.myanmar.obts.entity;

import android.os.Parcel;
import android.os.Parcelable;
import com.myanmar.obts.entity.City;
import com.myanmar.obts.entity.Route;

import java.util.List;

/**
 * Created by minh on 1/12/2015.
 */
public class TicketPackage implements Parcelable {

    City toCity;
    City fromCity;
    String routeDate;
    Route route;
    String SelectedSeats;
    String BaseFare;
    String BoardingPoint;
    String DroppingPoint;


    public City getToCity(){return toCity;}
    public City getFromCity(){return fromCity;}
    public String getRouteDate(){return routeDate;}
    public Route getRoute(){return route;}
    public String getSelectedSeats(){return SelectedSeats;}
    public String getBaseFare(){return BaseFare;}
    public String getBoardingPoint(){return BoardingPoint;}
    public String getDroppingPoint(){return DroppingPoint;}

    public void setToCity(City _toCity){toCity=_toCity;}
    public void setFromCity(City _fromCity){fromCity=_fromCity;}
    public void setRouteDate(String _routeDate){routeDate=_routeDate;}
    public void setRoute(Route _route){route=_route;}
    public void setSelectedSeats(String _selectedSeats){SelectedSeats=_selectedSeats;}
    public void setBaseFare(String _baseFare){BaseFare=_baseFare;}
    public void setBoardingPoint(String _BoardingPoint){BoardingPoint=_BoardingPoint;}
    public void setDroppingPoint(String _DroppingPoint){DroppingPoint=_DroppingPoint;}

    @Override
    public int describeContents() {
        // TODO Auto-generated method stub
        return 0;
    }


    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeParcelable(toCity, flags);
        dest.writeParcelable(fromCity,flags);
        dest.writeString(routeDate);
        dest.writeParcelable(route, flags);
        dest.writeString(SelectedSeats);
        dest.writeString(BaseFare);
        dest.writeString(BoardingPoint);
        dest.writeString(DroppingPoint);
        //dest.writeList(seats);
    }

    private TicketPackage(Parcel in){
        this.toCity =(City) in.readParcelable(City.class.getClassLoader());
        this.fromCity = (City) in.readParcelable(City.class.getClassLoader());
        this.routeDate=in.readString();
        this.route=(Route) in.readParcelable(Route.class.getClassLoader());
        this.SelectedSeats=in.readString();
        this.BaseFare=in.readString();
        this.BoardingPoint=in.readString();
        this.DroppingPoint=in.readString();
       // in.readList(this.seats,SeatItems.class.getClassLoader());
    }

    public static final Parcelable.Creator<TicketPackage> CREATOR = new Parcelable.Creator<TicketPackage>() {

        @Override
        public TicketPackage createFromParcel(Parcel source) {
            return new TicketPackage(source);
        }

        @Override
        public TicketPackage[] newArray(int size) {
            return new TicketPackage[size];
        }
    };

    public TicketPackage(City _toCity,City _fromCity,String _routeDate,Route _route,String _SelectedSeats,String _BaseFare,String _BoardingPoint,String _DroppingPoint){
        this.toCity = _toCity;
        this.fromCity = _fromCity;
        this.routeDate=_routeDate;
        this.route=_route;
        this.SelectedSeats=_SelectedSeats;
        this.BaseFare=_BaseFare;
        this.BoardingPoint=_BoardingPoint;
        this.DroppingPoint=_DroppingPoint;
        //this.seats=_seats;
    }
}

