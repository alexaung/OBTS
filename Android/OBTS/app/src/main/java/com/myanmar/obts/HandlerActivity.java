package com.myanmar.obts;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.view.ViewParent;
import android.view.Window;
import android.widget.Button;
import android.widget.TextView;

import com.myanmar.obts.obts.R;

public class HandlerActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_handler);
        //Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        //setSupportActionBar(toolbar);

        Bundle b=getIntent().getExtras();
        String Message=b.getString("Message");
        if(Message==null ||  Message.isEmpty()) finish();
        else {
            Message = (isOnline() ? (Message.contains("failed to") ? "Failed to connect server." : Message) : "No internet.");

            TextView txtMessage = (TextView) findViewById(R.id.txtMessage);
            txtMessage.setText(Message);
        }
        Button btnRetry=(Button)findViewById(R.id.btnReTry);
        btnRetry.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent();
                setResult(RESULT_OK, intent);
                finish();
            }
        });
    }

    public boolean isOnline() {
        ConnectivityManager cm =
                (ConnectivityManager) getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo netInfo = cm.getActiveNetworkInfo();
        return netInfo != null && netInfo.isConnectedOrConnecting();
    }

}
