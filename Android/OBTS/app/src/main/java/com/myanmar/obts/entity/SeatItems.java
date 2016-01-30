package com.myanmar.obts.entity;

import android.os.Parcel;
import android.os.Parcelable;

import java.util.List;

public class SeatItems implements Parcelable {

    private String SeatDetailId;
    public String getSeatDetailId(){return SeatDetailId;}
    public void setSeatDetailId(String _SeatDetailId){this.SeatDetailId=_SeatDetailId;}

    private String BusId;
    public String getBusId(){return BusId;}
    public void setBusId(String _BusId){this.BusId=_BusId;}

    private String RouteId;
    public String getRouteId(){return RouteId;}
    public void setRouteId(String _RouteId){this.RouteId=_RouteId;}

    private String Company;
    public String getCompany(){return Company;}
    public void setCompany(String _Company){this.Company=_Company;}

    private String BrandDesc;
    public String getBrandDesc(){return BrandDesc;}
    public void setBrandDesc(String _BrandDesc){this.BrandDesc=_BrandDesc;}

    private String BusTypeDesc;
    public String getBusTypeDesc(){return BusTypeDesc;}
    public void setBusTypeDesc(String _BusTypeDesc){this.BusTypeDesc=_BusTypeDesc;}

    private String BusRegistrationNo;
    public String getBusRegistrationNo(){return BusRegistrationNo;}
    public void setBusRegistrationNo(String _BusREgistrationNo){this.BusRegistrationNo=_BusREgistrationNo;}

    private boolean Bookable;
    public boolean getBookable(){return Bookable;}
    public void setBookable(boolean _Bookable){Bookable=_Bookable;}

    private boolean Space;
    public boolean getSpace(){return Space;}
    public void setSpace(boolean _Space){Space=_Space;}

    private boolean SpecialSeat;
    public boolean getSpecialSeat(){return SpecialSeat;}
    public void setSpecialSeat(boolean _SpecialSeat){SpecialSeat=_SpecialSeat;}

    private boolean Status;
    public boolean getStatus(){return Status;}
    public void setStatus(boolean _Status){Status=_Status;}

    private short UpperLower;
    public short getUpperLower(){return UpperLower;}
    public void setUpperLower(short _UpperLower){UpperLower=_UpperLower;}

    private int Row;
    private int Col;
    private String SeatNo;
    private short State;

    public short getState(){
       return State;
    }
    public void setState(short _State){this.State=_State;}

    public String getSeatNo(){return SeatNo;}

    public void setSeatNo(String _SeatNo){this.SeatNo=_SeatNo;}

    public int getRow() {
        return Row;
    }

    public void setRow(int _Row) {
        this.Row = _Row;
    }

    public int getCol() {
        return Col;
    }

    public void setCol(int _Col) {
        this.Col = _Col;
    }

    @Override
    public int describeContents() {
        // TODO Auto-generated method stub
        return 0;
    }


    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(SeatDetailId);
        dest.writeString(BusId);
        dest.writeString(RouteId);
        dest.writeString(Company);
        dest.writeString(BrandDesc);
        dest.writeString(BusTypeDesc);
        dest.writeString(BusRegistrationNo);
        dest.writeByte((byte) (Bookable ? 1 : 0));
        dest.writeByte((byte) (Space ? 1 : 0));
        dest.writeByte((byte) (SpecialSeat ? 1 : 0));
        dest.writeByte((byte) (Status ? 1 : 0));
        dest.writeInt((int) UpperLower);
        dest.writeInt(Row);
        dest.writeInt(Col);
        dest.writeString(SeatNo);
        dest.writeInt((int) State);
    }

    private SeatItems(Parcel in){
        this.SeatDetailId = in.readString();
        this.BusId = in.readString();
        this.RouteId = in.readString();
        this.Company = in.readString();
        this.BrandDesc=in.readString();
        this.BusTypeDesc = in.readString();
        this.BusRegistrationNo = in.readString();
        Bookable = in.readByte() != 0;
        Space= in.readByte() != 0;
        SpecialSeat= in.readByte() != 0;
        Status= in.readByte() != 0;
        UpperLower=(short)in.readInt();
        Row=in.readInt();
        Col=in.readInt();
        SeatNo=in.readString();
        State=(short)in.readInt();
    }

    public static final Parcelable.Creator<SeatItems> CREATOR = new Parcelable.Creator<SeatItems>() {

        @Override
        public SeatItems createFromParcel(Parcel source) {
            return new SeatItems(source);
        }

        @Override
        public SeatItems[] newArray(int size) {
            return new SeatItems[size];
        }
    };

    public SeatItems(String _SeatDetailId,String _BusId,
                     String _RouteId,String _Company,
                     String _BrandDesc,String _BusTypeDesc,
                     String _BusRegistrationNo,
                     boolean _Bookable,boolean _Space,
                     boolean _SpecialSeat,boolean _Status,
                     short _UpperLower,int _Row,int _Col,
                     String _SeatNo,short _State){
        this.SeatDetailId = _SeatDetailId;
        this.BusId = _BusId;        this.RouteId = _RouteId;
        this.Company = _Company;        this.BrandDesc=_BrandDesc;
        this.BusTypeDesc = _BusTypeDesc;        this.BusRegistrationNo = _BusRegistrationNo;
        Bookable = _Bookable;
        Space= _Space;
        SpecialSeat= _SpecialSeat;
        Status= _Status;
        UpperLower=_UpperLower;
        Row=_Row;
        Col=_Col;
        SeatNo=_SeatNo;
        State=_State;
    }
}
