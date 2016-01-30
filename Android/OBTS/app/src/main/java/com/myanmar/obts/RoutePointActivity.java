package com.myanmar.obts;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;

import com.myanmar.obts.entity.RoutePointsRowItems;
import com.myanmar.obts.entity.DividerItemDecoration;
import com.myanmar.obts.entity.RecyclerRoutePointAdapter;
import com.myanmar.obts.entity.RoutesRowItems;
import com.myanmar.obts.entity.TicketPackage;
import com.myanmar.obts.obts.R;
import com.myanmar.obts.service.ServiceGenerator;

import java.io.IOException;
import java.util.ArrayList;

import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

public class RoutePointActivity extends AppCompatActivity {
    TicketPackage ticketPackage;
    RecyclerView recyclerView;
    ArrayList<RoutePointsRowItems> itemsList = new ArrayList<>();
    RecyclerRoutePointAdapter adapter;
    private static String LOG_TAG = "RecyclerViewActivity";
    ProgressDialog mDialog=null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_boarding_point);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        mDialog  = ProgressDialog.show(RoutePointActivity.this, "",
                "Loading. Please wait...", true);

        Bundle b = getIntent().getExtras();
        ticketPackage=(TicketPackage)b.getParcelable("Package");


        recyclerView = (RecyclerView) findViewById(R.id.recyclerView);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        adapter = new RecyclerRoutePointAdapter(RoutePointActivity.this,itemsList );
        recyclerView.setAdapter(adapter);

        RecyclerView.ItemDecoration itemDecoration =
                new DividerItemDecoration(this, LinearLayoutManager.VERTICAL);
        recyclerView.addItemDecoration(itemDecoration);


        loadData();

        AppCompatButton btnBack=(AppCompatButton)findViewById(R.id.btnBack);
        btnBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onBackPressed();
            }
        });


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

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        overridePendingTransition(R.anim.fadein, R.anim.fadeout);
    }

    @Override
    protected void onResume() {
        super.onResume();
        ((RecyclerRoutePointAdapter) adapter).setOnItemClickListener(new RecyclerRoutePointAdapter.MyClickListener() {
            @Override
            public void onItemClick(int position, View v) {
                 Intent myIntent = new Intent(RoutePointActivity.this, ContactDetailsActivity.class);
                 Log.i(LOG_TAG, " Clicked on Item " + position);
                 RoutePointsRowItems item = (RoutePointsRowItems) itemsList.get(position);
                 Bundle b = new Bundle();
                 ticketPackage.setBoardingPoint(item.getBoardingPoint());
                 ticketPackage.setDroppingPoint(item.getDroppingPoint());
                 b.putParcelable("Package", ticketPackage);

                 myIntent.putExtras(b);
                 RoutePointActivity.this.startActivity(myIntent);
                 //setResult(RESULT_OK, intent);
                 //finish();
            }
        });
    }

    private void loadData(){
        ArrayList<RoutePointsRowItems> it = new ArrayList<RoutePointsRowItems>();

        ServiceGenerator.RoutesPointService client = ServiceGenerator.createService(ServiceGenerator.RoutesPointService.class);//,"ncs\\minh","P@ssw0rd");
        // Fetch and print a list of the contributors to this library.
        Call<ArrayList<RoutePointsRowItems>> call=  client.getRoutePoints(ticketPackage.getRoute().getRouteId());
        call.enqueue(new Callback<ArrayList<RoutePointsRowItems>>() {
            @Override
            public void onResponse(Response<ArrayList<RoutePointsRowItems>> response, Retrofit retrofit) {
                if (response.isSuccess()) {
                    //Response success. Handle data here
                    itemsList = response.body();
                    adapter.updateData(itemsList);
                    mDialog.hide();
                } else {
                    //For getting error message
                    Log.d("Error message", response.raw().message().toString());
                    //For getting error code. Code is integer value like 200,404 etc
                    Log.d("Error code", String.valueOf(response.raw().code()));
                    mDialog.hide();

                    Bundle b=getIntent().getExtras();
                    b.putString("Message", response.raw().message().toString());

                    Intent myIntent = new Intent(RoutePointActivity.this, HandlerActivity.class);

                    myIntent.putExtras(b);
                    RoutePointActivity.this.startActivityForResult(myIntent, 1);
                }
            }


            @Override
            public void onFailure(Throwable t) {
                if (t instanceof IOException) {
                    //Add your code for displaying no network connection error

                }
                mDialog.hide();
                Bundle b=getIntent().getExtras();
                b.putString("Message",t.getMessage());

                Intent myIntent = new Intent(RoutePointActivity.this, HandlerActivity.class);

                myIntent.putExtras(b);
                RoutePointActivity.this.startActivityForResult(myIntent,1);
            }
        });
        /*
        RoutePointsRowItems item1=new RoutePointsRowItems();
        item1.setId("1");
        item1.setTitle("Boarding Point 1");
        it.add(item1);

        RoutePointsRowItems item2=new RoutePointsRowItems();
        item2.setId("2");
        item2.setTitle("Boarding Point 2");
        it.add(item2);
        */

    }

}
