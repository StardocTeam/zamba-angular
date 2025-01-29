import { Pipe, PipeTransform } from '@angular/core';
import { Report } from './entitie/report';
import { TreeNode } from './report-component.component';

@Pipe({
    name: 'filter'
})
export class ReportFilterPipe implements PipeTransform {
    transform(items: any[], searchText: string): any[] {
        debugger;
        if (!items) return [];
        if (!searchText) return items;
        searchText = searchText.toLowerCase();
        return items.filter(item => {
            return item.Name.toLowerCase().includes(searchText);
        });
    }
}