import { VehicleStockImage } from "./vehicleStockImage";

export class VehicleStockItem {
  id: number;
  isDeleted: boolean;
  createdDate: string;
  modifiedDate: string;
  registrationNumber: string;
  vinNumber: string;
  createdBy: string;
  manufacturer: string;
  modelDescription: string;
  modelYear: number;
  currentKilometreReading: number;
  colour: string;
  retailPrice: number;
  costPrice: number;
  accessories: Accessory[];
  images: VehicleStockImage[];
  primaryImageSrc: string = '';
}

export class Accessory {
  name: string;
}
