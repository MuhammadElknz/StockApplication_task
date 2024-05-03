import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection: signalR.HubConnection | undefined;

  private stockListSource = new BehaviorSubject<any[]>([]);
  stockList$ = this.stockListSource.asObservable();

  constructor(private auth: AuthService) {}

  startConnection() {
    const token = this.auth.getAuthToken();
    if (token) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(environment.hubUrl, {
          accessTokenFactory: () => token,
        })
        .withAutomaticReconnect()
        .build();

      this.hubConnection
        .start()
        .then(() => this.joinStocksGroup())
        .catch((err) => console.log('Error while starting connection: ' + err));
    }
  }

  joinStocksGroup(): void {
    this.hubConnection
      ?.invoke('joinStocksGroup')
      .catch((err) => console.error('Error while joining group:', err));
  }

  addUpdatedStocksListener(callback: (stocks: any) => void) {
    this.hubConnection?.on('NotifyAllStocksPrices', (stocks: any) => {
      this.stockListSource.next(stocks);
    });
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection?.stop();
    }
  }
}
