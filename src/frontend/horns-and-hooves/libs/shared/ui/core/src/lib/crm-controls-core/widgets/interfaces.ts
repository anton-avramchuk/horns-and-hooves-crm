import { Type } from "@angular/core";

export interface IWigetConfiguration {
  component: Type<any>; // сам Angular-компонент
  allowForUnauthorized?: boolean; // показывать гостям
  roles?: string[]; // роли, которым виджет доступен
  order?: number; // порядок отображения (по возрастанию)
  tooltip?: string; // подсказка при наведении
  cssClass?: string; // кастомный CSS-класс
  featureFlag?: string; // ключ для включения через feature toggle
}
