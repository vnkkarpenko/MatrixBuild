class MatrixApp extends React.Component{

    constructor(props){
        super(props);
        this.state = { matrixData: [], rowAndCol: [5,1,2,3,4,6,7,8,9], selectedFile: "" };
 
        this.onRandomMatrix = this.getRandomMatrix.bind(this);
        this.onRotateMatrix = this.getRotateMatrix.bind(this);
        this.onImportMatrix = this.postImportMatrix.bind(this);
        this.onExportMatrix = this.postExportMatrix.bind(this);
        this.onSelectedFile = this.onSelectedFile.bind(this);
    }

    componentDidMount() {
        this.getRandomMatrix();
    }

    
    onSelectedFile () {
        const file = event.target.files[0];
        this.setState({ selectedFile: file });
    }

    getRandomMatrix() {
        this.setState({ matrixData: [] }, function() {
            const row = this.refs.rowselect.value;
            const col = this.refs.colselect.value;

            var xhr = new XMLHttpRequest();
            xhr.open("get", `${this.props.getRandomMxUrl}/${row}/${col}`, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    const data = JSON.parse(xhr.responseText);
                    console.log(data);
                    this.setState({ matrixData: data });
                }
            }.bind(this);
            xhr.send();
        }.bind(this));
    }

    getRotateMatrix() {
        this.setState({ matrixData: [] }, function() {
            var xhr = new XMLHttpRequest();
            xhr.open("get", this.props.getRotateMxUrl, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    const data = JSON.parse(xhr.responseText);
                    this.setState({ matrixData: data });
                }
            }.bind(this);
            xhr.send();
        }.bind(this));
    }

    postImportMatrix() {
        this.setState({ matrixData: [] }, function() {
            const formData = new FormData();
            const xhr = new XMLHttpRequest();
            formData.append("fileUpload", this.state.selectedFile);
            xhr.open("POST", this.props.importUrl, true);
            xhr.onload = () => {
                if (xhr.status === 200) {
                    const data = JSON.parse(xhr.responseText);
                    this.setState({ matrixData: data });
                }
            };
            xhr.send(formData);
        }.bind(this));
    }

    postExportMatrix() {
        var request = new XMLHttpRequest();
        request.open("POST", this.props.exportUrl, true);
        request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
        request.responseType = "blob";

        request.onload = function() {
            if (this.status === 200) {
                const blob = this.response;
                const downloadLink = window.document.createElement("a");
                const contentTypeHeader = request.getResponseHeader("Content-Type");
                downloadLink.href = window.URL.createObjectURL(new Blob([blob], { type: contentTypeHeader }));
                downloadLink.download = "MatrixExport.csv";
                document.body.appendChild(downloadLink);
                downloadLink.click();
                document.body.removeChild(downloadLink);
            }
        };
        request.send();
    }

    render(){
 
        return (
            <div className="container">
                <div className="row">
                    <h2><i className="glyphicon glyphicon-th"></i> Matrix build v1.00.2</h2>
                    <hr/>
                    <div className="col-md-offset-2 col-md-4 text-center">
                        <table className="table">
                            <tbody>{(this.state.matrixData || []).map(function(p, i) {
                                const animat = i % 2 === 0 ? "rotateIn" : "flip";

                                return (<tr><td className={"animated " + animat}>{p}</td></tr>);
                                }
                            )}
                            </tbody>
                        </table>
                    </div>
                    <div className="col-md-offset-1 col-md-3 text-center">
                        <button className="btn btn-default" onClick={this.onRandomMatrix}><i className="glyphicon glyphicon-random"></i> Random matrix</button>
                        <button className="btn btn-default" onClick={this.onRotateMatrix}><i className="glyphicon glyphicon-repeat"></i> Rotate matrix</button>
                        <button className="btn btn-default" onClick={this.onExportMatrix}><i className="glyphicon glyphicon-export"></i> Export matrix</button>
                        <button className="btn btn-default" onClick={this.onImportMatrix}><i className="glyphicon glyphicon-import"></i> Import matrix</button>
                        <input name="file" ref="fileUpload" type="file" onChange={this.onSelectedFile} />
                        <hr/>
                        <div className="row">
                            <div className="col-md-6">
                                <label>Row</label>
                                <select ref="rowselect" className="form-control" onChange={this.onRandomMatrix}>
                                    {(this.state.rowAndCol).map(function(i) {
                                        return (<option value={i}>{i}</option>);
                                    })}
                                </select>
                            </div>
                            <div className="col-md-6">
                                <label>Col</label>
                                <select ref="colselect" className="form-control" onChange={this.onRandomMatrix}>
                                    {(this.state.rowAndCol).map(function(i) {
                                        return (<option value={i}>{i}</option>);
                                    })}
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
            </div>);
    }
}
 
ReactDOM.render(
  <MatrixApp getRandomMxUrl="/api/GetRandomMatrix" getRotateMxUrl="/api/GetRotateMatrix" importUrl="/api/PostImportMatrix" exportUrl="/api/PostExportMatrix" />,
  document.getElementById("matrixcontent")
);