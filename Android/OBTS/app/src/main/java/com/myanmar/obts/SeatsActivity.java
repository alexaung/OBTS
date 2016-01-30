package com.myanmar.obts;


import android.content.Intent;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.myanmar.obts.entity.SeatItems;
import com.myanmar.obts.entity.SeatState;
import com.myanmar.obts.entity.TicketPackage;
import com.myanmar.obts.obts.R;

import java.util.List;

public class SeatsActivity extends AppCompatActivity {

    TicketPackage ticketPackage;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_seats);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        /*
        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });
        */

        Bundle b = getIntent().getExtras();
        ticketPackage =(TicketPackage) b.getParcelable("Package");

        TextView txtSeatTitle2=(TextView)findViewById(R.id.txtSeatTitle2);
        txtSeatTitle2.setText(ticketPackage.getRoute().getCompany());

        Button btnBookingSeatDone=(Button)findViewById(R.id.btnBookingSeatDone);
        btnBookingSeatDone.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                TextView txtSelectedSeatNumbers=(TextView) findViewById(R.id.txtSelectedSeatNumbers);
                TextView txtBaseFare=(TextView)findViewById(R.id.txtBaseFare);

                Intent myIntent = new Intent(SeatsActivity.this, RoutePointActivity.class);
                Bundle b = new Bundle();
                ticketPackage.setSelectedSeats(txtSelectedSeatNumbers.getText().toString());
                ticketPackage.setBaseFare(txtBaseFare.getText().toString());
                b.putParcelable("Package",ticketPackage);
                myIntent.putExtras(b);
                SeatsActivity.this.startActivity(myIntent);
            }
        });

        DrawSeatsLayout();
    }

    private void DrawSeatsLayout(){
        List<SeatItems> seats=ticketPackage.getRoute().getseats();
        TableLayout tblSeatsLayout=(TableLayout)findViewById(R.id.tblSeatsLayout);
        int nrow=1;
        TableRow row=new TableRow(getApplicationContext());

        for(int i=0;i<seats.size();i++)
        {
            SeatItems item=seats.get(i);
            if(item.getRow()==nrow)
            {

            }else {
                tblSeatsLayout.addView(row);
                row = new TableRow(getApplicationContext());
                nrow=item.getRow();
            }
            ImageButton btnSeat = new ImageButton(this);
            btnSeat.setAdjustViewBounds(true);
            btnSeat.setClickable(true);
            btnSeat.setScaleType(ImageView.ScaleType.FIT_CENTER);
            btnSeat.setBackground(ContextCompat.getDrawable(getApplicationContext(),
                    (item.getState() == SeatState.Available.getValue() ? R.mipmap.available_seat :
                            (item.getState() == SeatState.NotApplicable.getValue() ? R.mipmap.na_seat :
                                    (item.getState() == SeatState.Sold.getValue() ? R.mipmap.sold_seat :
                                            (item.getState() == SeatState.Space.getValue() ? R.mipmap.space_seat : R.mipmap.space_seat))))));
            btnSeat.setTag(R.id.TAG_SEAT_ID, item.getSeatNo());
            btnSeat.setTag(R.id.TAG_SEATSTATE,item.getState());
            btnSeat.setTag(R.id.TAG_SEAT_SELECTED,false);
            btnSeat.setOnClickListener(btnclick);



            row.addView(btnSeat);
        }
        if(seats.size()>1)
            tblSeatsLayout.addView(row);
    }

    ImageButton.OnClickListener btnclick = new ImageButton.OnClickListener(){
        @Override
        public void onClick(View v) {
            // TODO Auto-generated method stub

            ImageButton button = (ImageButton)v;
            String SeatNumber = (String) button.getTag(R.id.TAG_SEAT_ID);
            short seatState=(short)button.getTag(R.id.TAG_SEATSTATE);
            //= (SeatState)( (int) button.getTag(R.id.TAG_SEATSTATE));

            TextView txtBaseFare=(TextView)findViewById(R.id.txtBaseFare);
            String BaseFare=txtBaseFare.getText().toString();
            BaseFare=BaseFare.replace(ticketPackage.getRoute().getCurrencyDesc(),"").replace(" ","");
            float nBaseFare=(BaseFare.isEmpty()?0:Float.valueOf(BaseFare));

            if(seatState==SeatState.NotApplicable.getValue() ||
                    seatState==SeatState.Sold.getValue() ||
                    seatState==SeatState.Space.getValue())
                return;
            boolean isSelected = (boolean) button.getTag(R.id.TAG_SEAT_SELECTED);
            if(isSelected) {
                nBaseFare=nBaseFare-ticketPackage.getRoute().getPrice();
                txtBaseFare.setText(ticketPackage.getRoute().getCurrencyDesc()+" "+String.valueOf(nBaseFare));

                TextView txtSelectedSeatNumbers = (TextView) findViewById(R.id.txtSelectedSeatNumbers);
                String currentSeats=txtSelectedSeatNumbers.getText().toString();
                String[] seats= currentSeats.split(", ");
                currentSeats="";
                for(int i=0;i<seats.length;i++){
                    if(!seats[i].equals(SeatNumber))
                        currentSeats=(currentSeats.isEmpty()?seats[i]:currentSeats+", "+seats[i]);
                }
                txtSelectedSeatNumbers.setText(currentSeats);

                button.setBackground(ContextCompat.getDrawable(getApplicationContext(), R.mipmap.available_seat));
                button.setTag(R.id.TAG_SEAT_SELECTED, false);

                return;
            }

            TextView txtSelectedSeatNumbers = (TextView) findViewById(R.id.txtSelectedSeatNumbers);
            String preSeats = txtSelectedSeatNumbers.getText().toString();
            txtSelectedSeatNumbers.setText((preSeats.isEmpty() ? SeatNumber : preSeats + ", " + SeatNumber));

            button.setBackground(ContextCompat.getDrawable(getApplicationContext(), R.mipmap.selected_seat));
            button.setTag(R.id.TAG_SEAT_SELECTED, true);

            nBaseFare=nBaseFare+ticketPackage.getRoute().getPrice();
            txtBaseFare.setText(ticketPackage.getRoute().getCurrencyDesc()+" "+String.valueOf(nBaseFare));

        }

    };


/*
    View.OnClickListener getOnClickDoSomething(final ImageButton button)  {
        return new View.OnClickListener() {
            public void onClick(View v) {
                String SeatNumber = (String) button.getTag();
                TextView txtSelectedSeatNumbers = (TextView) findViewById(R.id.txtSelectedSeatNumbers);
                String preSeats = txtSelectedSeatNumbers.getText().toString();
                txtSelectedSeatNumbers.setText((preSeats.isEmpty() ? SeatNumber : preSeats + ", " + SeatNumber));
            }
        };
    }

    private ArrayList<SeatItems> getSeats(){
        ArrayList<SeatItems> seats= new ArrayList<SeatItems>();
        SeatItems item1=new SeatItems();
        item1.setRow(1);item1.setCol(1);item1.setSeatNo("1");
        item1.setState((short)SeatState.Available.getValue());
        seats.add(item1);

        SeatItems item2=new SeatItems();
        item2.setRow(1);item2.setCol(2);item2.setSeatNo("2");
        item2.setState((short)SeatState.Space.getValue());
        seats.add(item2);

        SeatItems item3=new SeatItems();
        item3.setRow(1);item3.setCol(3);item3.setSeatNo("3");
        item3.setState((short)SeatState.Available.getValue());
        seats.add(item3);

        SeatItems item4=new SeatItems();
        item4.setRow(2);item4.setCol(1);item4.setSeatNo("4");
        item4.setState((short)SeatState.Sold.getValue());
        seats.add(item4);

        SeatItems item5=new SeatItems();
        item5.setRow(2);item5.setCol(2);item5.setSeatNo("5");
        item5.setState((short)SeatState.Space.getValue());
        seats.add(item5);

        SeatItems item6=new SeatItems();
        item6.setRow(2);item6.setCol(3);item6.setSeatNo("6");
        item6.setState((short)SeatState.Available.getValue());
        seats.add(item6);

        SeatItems item7=new SeatItems();
        item7.setRow(3);item7.setCol(1);item7.setSeatNo("7");
        item7.setState((short)SeatState.Sold.getValue());
        seats.add(item7);

        SeatItems item8=new SeatItems();
        item8.setRow(3);item8.setCol(2);item8.setSeatNo("8");
        item8.setState((short)SeatState.NotApplicable.getValue());
        seats.add(item8);

        SeatItems item9=new SeatItems();
        item9.setRow(3);item9.setCol(3);
        item9.setSeatNo("9");
        item9.setState((short)SeatState.Available.getValue());
        seats.add(item9);
        return seats;
    }
    */
}
