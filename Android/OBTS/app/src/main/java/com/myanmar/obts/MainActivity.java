package com.myanmar.obts;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.widget.AppCompatButton;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TableLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.myanmar.obts.entity.City;
import com.myanmar.obts.entity.TicketPackage;
import com.myanmar.obts.obts.R;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
    TicketPackage ticketPackage=new TicketPackage(null,null,null,null,null,null,null,null);
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

      /*  FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Snackbar.make(view, "Replace with your own action", Snackbar.LENGTH_LONG)
                        .setAction("Action", null).show();
            }
        });
        */

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);

        ////
        EditText txtFromCity = (EditText) findViewById(R.id.txtFromCity);
        txtFromCity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SearchCities("From", v);
            }
        });
        txtFromCity.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            public void onFocusChange(View v, boolean hasFocus) {
                if (hasFocus) {
                    SearchCities("From", v);
                }
            }
        });

        /*txtFromCity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                v.requestFocus();



                Intent myIntent = new Intent(MainActivity.this, CityListActivity.class);
                MainActivity.this.startActivity(myIntent);
            }
        });*/

        AppCompatButton btnSearch=(AppCompatButton)findViewById(R.id.btnSearch);
        btnSearch.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //EditText txtFromCity=(EditText)findViewById(R.id.txtFromCity);
                //EditText txtToCity=(EditText)findViewById(R.id.txtToCity);
                SearchRoutes(v);
            }
        });

        EditText txtToCity = (EditText) findViewById(R.id.txtToCity);
        txtToCity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SearchCities("To", v);
            }
        });
        txtToCity.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            public void onFocusChange(View v, boolean hasFocus) {
                if (hasFocus) {
                    SearchCities("To", v);
                }
            }
        });

        TableLayout layoutMiddle = (TableLayout) findViewById(R.id.layoutMiddle);
        layoutMiddle.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //v.setBackgroundResource(R.drawable.group_box_bold);
                LinearLayout layoutTop = (LinearLayout) findViewById(R.id.layoutTop);
                layoutTop.setBackgroundResource(R.drawable.group_box);

                Intent myIntent = new Intent(MainActivity.this, CalendarActivity.class);
                Bundle b = new Bundle();
                b.putParcelable("Package", ticketPackage);
                myIntent.putExtras(b);
                MainActivity.this.startActivityForResult(myIntent, 2);
            }
        });

        ImageView imgSwapCity=(ImageView)findViewById(R.id.imgSwapCity);
        imgSwapCity.setOnClickListener(new View.OnClickListener() {
               @Override
               public void onClick(View v) {
                   EditText txtFromCity=(EditText) findViewById(R.id.txtFromCity);
                   EditText txtToCity=(EditText) findViewById(R.id.txtToCity);
                   City from=ticketPackage.getFromCity();
                   City to=ticketPackage.getToCity();
                   City city=new City(from.getId(),from.getName());
                   from=to;
                   to=city;
                   txtFromCity.setText(from.getName());
                   txtToCity.setText(to.getName());
                   ticketPackage.setFromCity(from);
                   ticketPackage.setToCity(to);
               }
           });

        Calendar cal=Calendar.getInstance();
        SimpleDateFormat sdf = new SimpleDateFormat("dd-MM-yyyy");
        String currentDate = sdf.format(new Date());
        setCalenderDate(currentDate);
/*
        LinearLayout layoutTop=(LinearLayout)findViewById(R.id.layoutTop);
        layoutTop.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                v.setBackgroundResource(R.drawable.group_box_bold);
                TableLayout layoutMiddle=(TableLayout) findViewById(R.id.layoutMiddle);
                layoutMiddle.setBackgroundResource(R.drawable.group_box);
            }
        });*/
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        //if (id == R.id.action_settings) {
        //    return true;
        //}

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {
            // Handle the camera action
        } else if (id == R.id.nav_gallery) {

        } else if (id == R.id.nav_slideshow) {

        } else if (id == R.id.nav_manage) {

        } else if (id == R.id.nav_share) {

        } else if (id == R.id.nav_send) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    private void SearchRoutes(View v){
        if(ticketPackage.getFromCity()==null || ticketPackage.getToCity()==null)
        {
            Toast.makeText(this,"Please enter 'From' and 'To' cities.",Toast.LENGTH_SHORT).show();
            return;
        }
        Intent myIntent = new Intent(MainActivity.this, RoutesActivity.class);
        Bundle b = new Bundle();

        //ticketPackage.setRouteDate("01-12-2015");
        b.putParcelable("Package", ticketPackage);
        myIntent.putExtras(b);
        MainActivity.this.startActivity(myIntent);
    }

    private void SearchCities(String Caller, View v) {
        //TableLayout layoutMiddle = (TableLayout) findViewById(R.id.layoutMiddle);
        //layoutMiddle.setBackgroundResource(R.drawable.group_box);

        LinearLayout layoutTop = (LinearLayout) findViewById(R.id.layoutTop);
        layoutTop.setBackgroundResource(R.drawable.group_box_bold);


        Intent myIntent = new Intent(MainActivity.this, CitiesSearchActivity.class);
        Bundle b = new Bundle();
        b.putString("Caller", Caller);
        myIntent.putExtras(b);
        MainActivity.this.startActivityForResult(myIntent, 1);

    }

    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 1) {
            if (resultCode == RESULT_OK) {
                Bundle b = data.getExtras();
                City city=b.getParcelable("SelectedCity");
                String Caller = b.getString("Caller");

                EditText txtFromCity=(EditText) findViewById(R.id.txtFromCity);
                EditText txtToCity=(EditText) findViewById(R.id.txtToCity);

                if(Caller.equals("From")) {
                    txtFromCity.setText(city.getName());
                    ticketPackage.setFromCity(city);
                }
                if(Caller.equals("To")) {
                    txtToCity.setText(city.getName());
                    ticketPackage.setToCity(city);
                }
            }
        }else  if (requestCode == 2) {
            Bundle b = data.getExtras();
            ticketPackage=(TicketPackage)b.getParcelable("Package");
            setCalenderDate(ticketPackage.getRouteDate());
        }
    }

    private void setCalenderDate(String _sDate)
    {
        String[] sDate=_sDate.split("-");

        Calendar cal=Calendar.getInstance();
        cal.set(Integer.valueOf(sDate[2]), Integer.valueOf(sDate[1])-1, Integer.valueOf(sDate[0]));
        String sDayName= new SimpleDateFormat("EEEE").format(cal.getTime());
        String sMonth= new SimpleDateFormat("MM").format(cal.getTime());
        String sMonthName=new SimpleDateFormat("MMMM").format(cal.getTime());
        ticketPackage.setRouteDate(sDate[0]+"-"+sMonth+"-"+sDate[2]);
        TextView txtDay=(TextView)findViewById(R.id.txtDay);
        txtDay.setText(sDate[0]);

        TextView txtMonth=(TextView)findViewById(R.id.txtMonth);
        txtMonth.setText(sMonthName);

        TextView txtYear=(TextView)findViewById(R.id.txtYear);
        txtYear.setText(sDate[2]);

        TextView txtDayName=(TextView)findViewById(R.id.txtDayName);
        txtDayName.setText(sDayName);
    }
}
