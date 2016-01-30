package com.myanmar.obts;

import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.TextView;

import com.myanmar.obts.entity.TicketPackage;
import com.myanmar.obts.obts.R;

import org.w3c.dom.Text;

public class PaymentOptionsActivity extends AppCompatActivity {

    TicketPackage ticketPackage;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_payment_options);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        Bundle b=getIntent().getExtras();
        ticketPackage=(TicketPackage) b.getParcelable("Package");

        TextView txtRouteDescription=(TextView)findViewById(R.id.txtRouteDescription);
        txtRouteDescription.setText(ticketPackage.getFromCity().getName()+" To "+ticketPackage.getToCity().getName());

        TextView txtTripDate=(TextView)findViewById(R.id.txtTripDate);
        txtTripDate.setText(ticketPackage.getRouteDate());

        TextView txtBoardingPlaceAndTime=(TextView)findViewById(R.id.txtBoardingPlaceAndTime);
        txtBoardingPlaceAndTime.setText(getResources().getString(R.string.boardingfrom)+" "+ticketPackage.getBoardingPoint()+" at "+ticketPackage.getRoute().getDepartureTime());

        /*
        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });*/
    }

}
