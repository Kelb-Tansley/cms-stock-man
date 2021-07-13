import { Component, Inject, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { SelectionModel } from '@angular/cdk/collections';
import { MatSort } from '@angular/material/sort';
import { VehicleStockItem } from '../models/vehicleStockItem';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, Subscription } from "rxjs";
import { EmitEvent, EventBusService, Events } from '../services/event-bus.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-stock',
  styleUrls: ['stock.component.css'],
  templateUrl: 'stock.component.html',
})

export class StockComponent implements OnInit, OnDestroy {
  //#region Private Fields'
  http: HttpClient;
  baseUrl: string;
  showFiller = false;
  isDrawerOpened = false;

  eventbusSubscription: Subscription;
  stockCancelledSubscription: Subscription;
  paramsGetSubscription: Subscription;
  paramsPostSubscription: Subscription;

  displayedColumns: string[] = ['select', 'registrationNumber', 'vinNumber', 'manufacturer', 'modelDescription', 'modelYear', 'currentKilometreReading', 'colour', 'retailPrice', 'costPrice', 'image', 'edit'];
  dataSource: MatTableDataSource<VehicleStockItem>;
  selection = new SelectionModel<VehicleStockItem>(true, []);
  selectedItem: VehicleStockItem = new VehicleStockItem();
  vehicleStockData: VehicleStockItem[];

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  //#endregion

  //#region Constructor
  constructor(private eventbus: EventBusService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl
  }

  async ngOnInit() {
    this.selectedItem = new VehicleStockItem();

    this.eventbusSubscription = this.eventbus.on(Events.StockSubmitted, (stock => this.onStockSubmittedEvent(stock)));
    this.stockCancelledSubscription = this.eventbus.on(Events.StockCancelled, (() => this.onStockCancelledEvent()));

    await this.onGetAllVehicleStockItems();
  }

  async onGetAllVehicleStockItems() {
    let endPoint = this.baseUrl + 'vehiclestock';
    this.paramsGetSubscription = await this.http.get<VehicleStockItem[]>(endPoint).subscribe(result => {
      this.vehicleStockData = result;

      this.onSetPrimaryImageSrc(this.vehicleStockData);

      this.dataSource = new MatTableDataSource<VehicleStockItem>(this.vehicleStockData);
      //this.dataSource = new MatTableDataSource<VehicleStockItem>(ELEMENT_DATA);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }

  onSetPrimaryImageSrc(vehicleStockData: VehicleStockItem[]) {
    if (vehicleStockData) {
      vehicleStockData.forEach((vih: VehicleStockItem) => {
        if (vih.images) {
          if (vih.images.length > 0) {
            let imageStockItem = vih.images.find(i => i.isPrimary == true);
            if (imageStockItem) {
              vih.primaryImageSrc = imageStockItem.stockImage;
            }
          }
        }
      });
    }
  }

  onStockCancelledEvent() {
    this.isDrawerOpened = false;
  }

  async onStockSubmittedEvent(stock: VehicleStockItem) {
    if (stock && this.dataSource) {
      let endPoint = this.baseUrl + 'vehiclestock';
      this.paramsPostSubscription = await this.http.post<VehicleStockItem>(endPoint, stock).subscribe(result => {
        stock = result;

        this.isDrawerOpened = false;

        this.onGetAllVehicleStockItems();
      }, error => console.error(error));
      //this.addOrUpdateVehicleStockItem(stock);
    }
  }

  addOrUpdateVehicleStockItem(vehicleStockItem: VehicleStockItem): Observable<VehicleStockItem> {
    let endPoint = this.baseUrl + 'vehiclestock';
    return this.http.post<VehicleStockItem>(endPoint, vehicleStockItem);
  }

  // handleError(arg0: string, vehicleStockItem: VehicleStockItem): (err: any, caught: Observable<any>) => import("rxjs").ObservableInput<any> {
  //   throw new Error('Method not implemented.');
  // }
  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    // Return an observable with a user-facing error message.
    return throwError(
      'Something bad happened; please try again later.');
  }
  ngOnDestroy() {
    if (this.eventbusSubscription) {
      this.eventbusSubscription.unsubscribe();
    }
    if (this.stockCancelledSubscription) {
      this.stockCancelledSubscription.unsubscribe();
    }
    if (this.paramsGetSubscription) {
      this.paramsGetSubscription.unsubscribe();
    }
    if (this.paramsPostSubscription) {
      this.paramsPostSubscription.unsubscribe();
    }
  }
  //#endregion

  //#region Methods'
  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    if (this.dataSource) {
      const numRows = this.dataSource.data.length;
      return numSelected === numRows;
    }
    else {
      return false;
    }
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.dataSource.data.forEach(row => this.selection.select(row));
  }

  /** The label for the checkbox on the passed row */
  checkboxLabel(element?: VehicleStockItem): string {
    if (!element) {
      return `${this.isAllSelected() ? 'select' : 'deselect'} all`;
    }
    return `${this.selection.isSelected(element) ? 'deselect' : 'select'} row ${element.id + 1}`;
  }

  applyFilter(filterEvent: Event) {
    const filterValue = (filterEvent.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onAddStockDetailsCall() {
    this.selectedItem = new VehicleStockItem();
    this.eventbus.emit(new EmitEvent(Events.StockSelected, this.selectedItem));
  }
  onEditStockDetailsCall(element?: VehicleStockItem) {
    this.selectedItem = element;
    this.eventbus.emit(new EmitEvent(Events.StockSelected, this.selectedItem));
  }
  onDeleteSelectedItems() {
    this.selection.selected.forEach(function (value: VehicleStockItem) { value.isDeleted = true; });
    //this.dataSource.data.splice();
  }
  //#endregion
}

