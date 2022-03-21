import { Component, DoCheck, OnChanges, OnDestroy, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SignalRService } from './services/signal-r.service';
import { Population } from './models/population';
import { BaseChartDirective } from 'ng2-charts';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, DoCheck, OnDestroy {
  
  
  public testLabel: string[] = [];
  @ViewChild(BaseChartDirective) chart?: BaseChartDirective;
  show:boolean = true;

  title = 'Frontend';

  constructor(public signalR: SignalRService, private http:HttpClient) 
  {
    for (let index = 1950; index < 2101; index++)
    {
      this.testLabel.push(""+index);
    }
  }
  ngOnDestroy(): void {
    console.log("Closed component");
    this.signalR.closeConnection();
  }
  
  ngDoCheck(): void {
    this.chart?.update();
  }

  ngOnInit(): void 
  {
    this.signalR.startConnection();
    this.signalR.addTransferDataSetDataListener();
    this.startHttpRequest();
  }

  closeConnect()
  {
    this.signalR.closeConnection();
  }

  private startHttpRequest()
  {
    this.http.get<Population>("https://signalrpopulation.azurewebsites.net/api/chart")
      // .subscribe((response) => {console.log(response);this.show = false});
      .subscribe({
        next: (res) => {console.log(res); this.show=false;}, 
        error: () => this.show=false
      });
  }


}
