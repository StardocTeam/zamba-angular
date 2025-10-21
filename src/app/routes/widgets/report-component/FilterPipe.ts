import { Pipe, PipeTransform } from '@angular/core';
import { Report } from './entitie/report';

@Pipe({
    name: 'filter'
})
export class ReportFilterPipe implements PipeTransform {
    transform(reports: Report[] | undefined, searchValue: string): Report[] {
        if (!reports) {
            return [];
        }
        if (!searchValue) {
            return reports;
        }
        return reports.filter(report => report.Name.toLowerCase().includes(searchValue.toLowerCase()));
    }

    // transform(reports: Report[] | undefined, searchValue: string): { filteredReports: Report[], hasVisibleItems: boolean } {
    //     if (!reports) {
    //         return { filteredReports: [], hasVisibleItems: false };
    //     }
    //     if (!searchValue) {
    //         return { filteredReports: reports, hasVisibleItems: true };
    //     }
    //     const filteredReports = reports.filter(report => report.Name.toLowerCase().includes(searchValue.toLowerCase()));
    //     return { filteredReports, hasVisibleItems: filteredReports.length > 0 };
    // }
}