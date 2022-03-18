import { Injectable, ViewChild } from '@angular/core';
import { Population } from '../models/population';
import * as signalR from "@microsoft/signalr";
import { ChartDataset } from 'chart.js';
import { ConsoleLogger } from '@microsoft/signalr/dist/esm/Utils';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public dataset: ChartDataset[] = [
    {data:[], label:"Male Population", borderColor: 'rgba(1,111,185)', pointBackgroundColor: 'rgba(146,220,229)', backgroundColor:'rgba(146,220,229)'},
    {data:[], label: "Women Population", borderColor: 'rgba(224,176,213)', pointBackgroundColor: 'rgba(229, 208, 227)', backgroundColor: 'rgba(229, 208, 227)'}
  ];
  public label: string[] = [];
  public lastMaleValue:number;
  public lastFemaleValue:number;

  public title:string;

  private hubConnection: signalR.HubConnection

  constructor() 
  { 
    this.hubConnection = new signalR.HubConnectionBuilder()
                        .withUrl("https://signalrpopulation.azurewebsites.net/chart", {skipNegotiation:true, transport: signalR.HttpTransportType.WebSockets})
                        .build();
  }
  
  public startConnection()
  {
    this.hubConnection = new signalR.HubConnectionBuilder()
                        .withUrl("https://signalrpopulation.azurewebsites.net/chart", {skipNegotiation:true, transport: signalR.HttpTransportType.WebSockets})
                        .build();

    this.hubConnection.start()
      .then(() => console.log("Connection started"))
      .catch((err) => console.log("Error while starting connection "+err));
  }

  public closeConnection()
  {
    this.hubConnection.stop()
        .then(() =>  console.log("Connection stopped"))
        .catch((err) => console.log("Error with stopping connection " + err));
  }

  public addTransferDataSetDataListener()
  {
    this.hubConnection.on("transferPopulationData", (apidata : Population) => {
      this.lastMaleValue = apidata.malePopulation;
      this.lastFemaleValue = apidata.femalePopulation;
      this.dataset[0].data.push(apidata.malePopulation);
      this.dataset[1].data.push(apidata.femalePopulation);
      this.title = apidata.location;

      if (apidata.time < 2099) {
        this.label.push(apidata.time+"");
      }
      else
      {
        this.label = [];
      }
      console.log(apidata);
    });
  }

}
