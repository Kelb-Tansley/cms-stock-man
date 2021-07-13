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

  displayedColumns: string[] = ['select', 'id', 'registrationNumber', 'manufacturer', 'modelDescription', 'modelYear', 'edit'];
  dataSource: MatTableDataSource<VehicleStockItem>;
  selection = new SelectionModel<VehicleStockItem>(true, []);
  selectedItem: VehicleStockItem = new VehicleStockItem();
  vehicleStockData: VehicleStockItem[];
  paramsSubscription: Subscription;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  //#endregion

  //#region Constructor
  constructor(private eventbus: EventBusService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl
  }

  ngOnInit() {
    let endPoint = this.baseUrl + 'vehiclestock';
    this.selectedItem = new VehicleStockItem();

    this.eventbusSubscription = this.eventbus.on(Events.StockSubmitted, (stock => this.onStockSubmittedEvent(stock)));
    this.stockCancelledSubscription = this.eventbus.on(Events.StockCancelled, (() => this.onStockCancelledEvent()));

    this.paramsSubscription = this.http.get<VehicleStockItem[]>(endPoint).subscribe(result => {
      this.vehicleStockData = result;
      this.dataSource = new MatTableDataSource<VehicleStockItem>(this.vehicleStockData);
      //this.dataSource = new MatTableDataSource<VehicleStockItem>(ELEMENT_DATA);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }


  onStockCancelledEvent() {
    this.isDrawerOpened = false;
  }

  onStockSubmittedEvent(stock: VehicleStockItem) {
    if (stock && this.dataSource) {
      let endPoint = this.baseUrl + 'vehiclestock';
      this.paramsSubscription = this.http.post<VehicleStockItem>(endPoint, stock).subscribe(result => {
        //this.dataSource.data.push(result);
        stock = result;
        //this.dataSource._updateChangeSubscription();
        this.isDrawerOpened = false;
      }, error => console.error(error));
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
    this.paramsSubscription.unsubscribe();
    if (this.eventbusSubscription) {
      this.eventbusSubscription.unsubscribe();
    }
    if (this.stockCancelledSubscription) {
      this.stockCancelledSubscription.unsubscribe();
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


//const ELEMENT_DATA: VehicleStockItem[] = [
//  { id: 1, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DB11', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 54000, colour: 'Red', vinNumber: '500TT5148', retailPrice: 3240000, costPrice: 981818.181818182, createdDate: '44388.8182322917', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 2, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DB11', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 54000, colour: 'Green', vinNumber: '500TT5148', retailPrice: 3240000, costPrice: 981818.181818182, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 3, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DB11 V8', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 36000, colour: 'Blue', vinNumber: '562TT5348', retailPrice: 1810800, costPrice: 548727.272727273, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 4, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DB11 V8', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 36000, colour: 'Orange', vinNumber: '562TT5348', retailPrice: 1810800, costPrice: 548727.272727273, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 5, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DBS', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 54000, colour: 'Yellow', vinNumber: '7002PT7056', retailPrice: 3861000, costPrice: 1170000, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 6, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DBS', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 54000, colour: 'White', vinNumber: '7002PT7056', retailPrice: 3861000, costPrice: 1170000, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 7, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DBX', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 44000, colour: 'Black', vinNumber: '8001PT8342', retailPrice: 2420000, costPrice: 733333.333333333, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 8, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'DBX', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 44000, colour: 'Red', vinNumber: '8001PT8342', retailPrice: 2420000, costPrice: 733333.333333333, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 9, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'Vantage Manual', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 31000, colour: 'Green', vinNumber: '6031PT6102', retailPrice: 1559300, costPrice: 472515.151515151, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 10, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'Vantage Manual', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 31000, colour: 'Blue', vinNumber: '6031PT6102', retailPrice: 1559300, costPrice: 472515.151515151, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 11, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'Vantage V8', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 32000, colour: 'Orange', vinNumber: '600TT6106', retailPrice: 1609600, costPrice: 487757.575757576, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 12, registrationNumber: 'C44YZUGP', manufacturer: 'Aston Martin', modelDescription: 'Vantage V8', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 32000, colour: 'Yellow', vinNumber: '600TT6106', retailPrice: 1609600, costPrice: 487757.575757576, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 13, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15000, colour: 'White', vinNumber: '7F72606', retailPrice: 342000, costPrice: 103636.363636364, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 14, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15000, colour: 'Black', vinNumber: '7F72606', retailPrice: 342000, costPrice: 103636.363636364, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 15, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15000, colour: 'Red', vinNumber: '7F72606', retailPrice: 342000, costPrice: 103636.363636364, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 16, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15000, colour: 'Green', vinNumber: '7F72606', retailPrice: 342000, costPrice: 103636.363636364, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 17, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i xDrive Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15500, colour: 'Blue', vinNumber: '7D46114', retailPrice: 353400, costPrice: 107090.909090909, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 18, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i xDrive Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15500, colour: 'Orange', vinNumber: '7D46114', retailPrice: 353400, costPrice: 107090.909090909, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 19, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i xDrive Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15500, colour: 'Yellow', vinNumber: '7D46114', retailPrice: 353400, costPrice: 107090.909090909, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 20, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '228i xDrive Gran Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15500, colour: 'White', vinNumber: '7D46114', retailPrice: 353400, costPrice: 107090.909090909, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 21, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Convertible', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 16000, colour: 'Black', vinNumber: 'V646702', retailPrice: 396800, costPrice: 120242.424242424, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 22, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Convertible', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 16000, colour: 'Red', vinNumber: 'V646702', retailPrice: 396800, costPrice: 120242.424242424, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 23, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Convertible', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 16000, colour: 'Green', vinNumber: 'V646702', retailPrice: 396800, costPrice: 120242.424242424, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 24, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Convertible', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 16000, colour: 'Blue', vinNumber: 'V646702', retailPrice: 396800, costPrice: 120242.424242424, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 25, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Convertible', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 16000, colour: 'Orange', vinNumber: 'V646702', retailPrice: 396800, costPrice: 120242.424242424, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 26, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Convertible', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 16000, colour: 'Yellow', vinNumber: 'V646702', retailPrice: 396800, costPrice: 120242.424242424, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 27, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Convertible', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 16000, colour: 'White', vinNumber: 'V646702', retailPrice: 396800, costPrice: 120242.424242424, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 28, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 14500, colour: 'Black', vinNumber: 'V635700', retailPrice: 359600, costPrice: 108969.696969697, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 29, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 14500, colour: 'Red', vinNumber: 'V635700', retailPrice: 359600, costPrice: 108969.696969697, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 30, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 14500, colour: 'Green', vinNumber: 'V635700', retailPrice: 359600, costPrice: 108969.696969697, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 31, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230i Coupe', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 14500, colour: 'Blue', vinNumber: 'V635700', retailPrice: 359600, costPrice: 108969.696969697, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 32, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230I COUPE', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15000, colour: 'Orange', vinNumber: 'VD10025', retailPrice: 372000, costPrice: 112727.272727273, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 33, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230I COUPE', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15000, colour: 'Yellow', vinNumber: 'VD10025', retailPrice: 372000, costPrice: 112727.272727273, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 34, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230I COUPE', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 15000, colour: 'White', vinNumber: 'VD10025', retailPrice: 372000, costPrice: 112727.272727273, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 35, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230I XDRIVE CONVERTIBLE', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 17000, colour: 'Black', vinNumber: 'VD42012', retailPrice: 421600, costPrice: 127757.575757576, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 36, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230I XDRIVE CONVERTIBLE', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 17000, colour: 'Red', vinNumber: 'VD42012', retailPrice: 421600, costPrice: 127757.575757576, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },
//  { id: 37, registrationNumber: 'C44YZUGP', manufacturer: 'BMW', modelDescription: '230I XDRIVE CONVERTIBLE', createdBy: 'kelbys@hotmail.co.za', modelYear: 2021, currentKilometreReading: 17000, colour: 'Green', vinNumber: 'VD42012', retailPrice: 421600, costPrice: 127757.575757576, createdDate: '44388.8182321759', modifiedDate: '44388', accessories: [{ name: 'acc1', description: 'Desc1' }], images: [{ name: 'Image1', description: 'ImageDesc1' }] },

//];
