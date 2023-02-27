import ReactDOM from 'react-dom/client'
import App from './App/layout/App'
import { BaseStoreContext, storeContainer } from './App/Stores/BaseStore'
import './styles.css'

ReactDOM.createRoot(document.getElementById('root') as HTMLElement)
    .render(
        <BaseStoreContext.Provider value={storeContainer}>
            <App />
        </BaseStoreContext.Provider>
    )
