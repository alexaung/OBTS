package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.TextView;

import com.myanmar.obts.obts.R;

import java.util.ArrayList;

public class RecyclerRoutesAdapter extends RecyclerView.Adapter<RecyclerRoutesAdapter.DataObjectHolder> {
    Context context;
    ArrayList<RoutesRowItems> itemsList;
    private static MyClickListener myClickListener;
    private int lastPosition = -1;

    public static class DataObjectHolder extends RecyclerView.ViewHolder
            implements View
            .OnClickListener {
        TextView RouteTime;
        TextView RouteFare;
        TextView Company;
        TextView RouteDuration;
        TextView BusType;
        TextView TotalSeats;

        public DataObjectHolder(View itemView) {
            super(itemView);
            RouteTime = (TextView) itemView.findViewById(R.id.txtRouteTime);
            RouteFare = (TextView) itemView.findViewById(R.id.txtRouteFare);

            Company = (TextView) itemView.findViewById(R.id.txtCompany);
            RouteDuration = (TextView) itemView.findViewById(R.id.txtRouteDuration);

            BusType = (TextView) itemView.findViewById(R.id.txtBusType);
            TotalSeats = (TextView) itemView.findViewById(R.id.txtTotalSeats);

            itemView.setOnClickListener(this);
        }

        @Override
        public void onClick(View v) {
            myClickListener.onItemClick(getAdapterPosition(), v);
        }
    }

    public void setOnItemClickListener(MyClickListener myClickListener) {
        this.myClickListener = myClickListener;
    }

    //private SparseBooleanArray selectedItems;
    public RecyclerRoutesAdapter(Context context, ArrayList<RoutesRowItems> itemsList) {
        this.context = context;
        this.itemsList = itemsList;
    }

    @Override
    public int getItemCount() {
        if (itemsList == null) {
            return 0;
        } else {
            return itemsList.size();
        }
    }

    @Override
    public DataObjectHolder onCreateViewHolder(ViewGroup parent,
                                               int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.multi_rows, parent, false);

        DataObjectHolder dataObjectHolder = new DataObjectHolder(view);
        return dataObjectHolder;
    }

    public void updateData(ArrayList<RoutesRowItems> viewModels) {
        itemsList.clear();
        itemsList.addAll(viewModels);
        notifyDataSetChanged();
    }

    @Override
    public void onBindViewHolder(DataObjectHolder rowViewHolder, int position) {
        RoutesRowItems items = itemsList.get(position);
        rowViewHolder.RouteTime.setText(items.getDepartureTime()+" - "+items.getArrivalTime());
        rowViewHolder.RouteFare.setText(items.getCurrencyDesc()+" "+ String.valueOf(items.getRouteFare()));
        rowViewHolder.Company.setText(String.valueOf(items.getCompany()));
        rowViewHolder.RouteDuration.setText(String.valueOf(items.getRouteDuration()));
        rowViewHolder.BusType.setText(String.valueOf(items.getBusTypeDesc()));
        rowViewHolder.TotalSeats.setText(String.valueOf(items.getTotalSeats()));

       //rowViewHolder.myBackground.setSelected(selectedItems.get(position, false));
        setAnimation(((RecyclerView.ViewHolder) rowViewHolder).itemView, position);
    }

    public interface MyClickListener {
        public void onItemClick(int position, View v);
    }

    private void setAnimation(View viewToAnimate, int position)
    {
        // If the bound view wasn't previously displayed on screen, it's animated
        if (position > lastPosition)
        {
            Animation animation = AnimationUtils.loadAnimation(context, android.R.anim.slide_in_left);
            viewToAnimate.startAnimation(animation);
            lastPosition = position;
        }
    }
}