import { isExportSpecifier } from 'typescript';
import '../styling/HomePage.css';

export default function Error() {
    return (
        <div className="App">
            <header className="App-header">
                <div className="page">
                    <h1>An Error has occoured, please try again</h1>
                </div>
            </header>
        </div>
    );
}