import { Pipe, PipeTransform } from '@angular/core';
import { Report } from './entitie/report';

@Pipe({
    name: 'filter'
})
export class ReportFilterPipe implements PipeTransform {
    transform(items: Report[], searchText: string): any[] {
        if (!items) return [];
        if (!searchText) return items;
        searchText = searchText.toLowerCase();
        return items.filter(item => {
            return item.Name.toLowerCase().includes(searchText);
        });
    }
}