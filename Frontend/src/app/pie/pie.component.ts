import { Component, DoCheck, OnInit, ViewChild } from '@angular/core';
import { ChartConfiguration, ChartData } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { SignalRService } from '../services/signal-r.service';

@Component({
  selector: 'app-pie',
  templateUrl: './pie.component.html',
  styleUrls: ['./pie.component.css']
})
export class PieComponent implements OnInit, DoCheck {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective;

  public pieChartData: ChartData<'pie', number[], string | string[]> = {
    labels: [ [ 'Female' ], [ 'Male' ] ],
    datasets: [ {
      data: [ 300, 500 ]
    } ]
  };

  constructor(public signalR:SignalRService) {
  }

  ngDoCheck(): void {
    this.pieChartData.datasets[0].data[1] = this.signalR.lastMaleValue;
    this.pieChartData.datasets[0].data[0] = this.signalR.lastFemaleValue;
    this.chart?.update();
  }

  ngOnInit(): void {
  }

}
