package com.myanmar.obts.entity;

/**
 * Created by minh on 25/11/2015.
 */
import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.myanmar.obts.obts.R;

import java.util.ArrayList;

public class RecyclerRoutePointAdapter extends RecyclerView.Adapter<RecyclerRoutePointAdapter.DataObjectHolder> {
    Context context;
    ArrayList<RoutePointsRowItems> itemsList;
    private static MyClickListener myClickListener;

    public static class DataObjectHolder extends RecyclerView.ViewHolder
            implements View
            .OnClickListener {
        TextView BoardingTime;
        TextView DroppingTime;
        TextView BoardingPoint;
        TextView DroppingPoint;

        public DataObjectHolder(View itemView) {
            super(itemView);
            BoardingTime = (TextView) itemView.findViewById(R.id.txtBoardingTime);
            DroppingTime = (TextView) itemView.findViewById(R.id.txtDroppingTime);
            BoardingPoint=(TextView) itemView.findViewById(R.id.txtBoardingPoint);
            DroppingPoint=(TextView) itemView.findViewById(R.id.txtDroppingPoint);

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
    public RecyclerRoutePointAdapter(Context context, ArrayList<RoutePointsRowItems> itemsList) {
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

    public void updateData(ArrayList<RoutePointsRowItems> viewModels) {
        itemsList.clear();
        itemsList.addAll(viewModels);
        notifyDataSetChanged();
    }

    @Override
    public DataObjectHolder onCreateViewHolder(ViewGroup parent,
                                               int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.routepoint_row, parent, false);

        DataObjectHolder dataObjectHolder = new DataObjectHolder(view);
        return dataObjectHolder;
    }


    @Override
    public void onBindViewHolder(DataObjectHolder rowViewHolder, int position) {
        RoutePointsRowItems items = itemsList.get(position);
        rowViewHolder.BoardingPoint.setText(items.getBoardingPoint());
        rowViewHolder.BoardingTime.setText(items.getBoardingTime());
        rowViewHolder.DroppingPoint.setText(items.getDroppingPoint());
        rowViewHolder.DroppingTime.setText(items.getDroppingTime());
    }

    public interface MyClickListener {
        public void onItemClick(int position, View v);
    }
}