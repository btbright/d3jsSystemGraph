<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<SETArchitecture.UI.Models.ApplicationGraphModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ApplicationGraph
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
.link { stroke: #ccc; }
.nodetext { pointer-events: none; font: 10px sans-serif; }
#legend {
    width: 300px;
}
#legend div {
    height: 10px;
    width: 30px;
    margin: 0 20px;
    float: left;
    text-align: center;
}
#legend p {
    display: block;
    width: 30px;
    font-size: 12px;
    font-family: sans-serif;
}
h3 { font-size: 14px;
    font-weight: bold;}
    #chart { float: left;}
</style>
    <h2>d3.js ApplicationGraph</h2>
    <script>
        var applicationJSON = '<%= Model.json%>';
    </script>
    <div id="legend">
        <h3>Application Types</h3>
        <div style="background:#42FFDF">
            <p>.NET WebForms</p>
        </div>
        <div style="background:#208DCC">
            <p>.NET MVC</p>
        </div>
        <div style="background:#658699">
            <p>Classic ASP</p>
        </div>
        <div style="background:#3D9998">
            <p>Web Service</p>
        </div>
        <br style="clear:both;"/>
        <br style="clear:both;"/>
        <h3>Server Types</h3>
        <div style="background:#FF9D75">
            <p>Web Server</p>
        </div>
        <div style="background:#CC6756">
            <p>Database Server</p>
        </div>
    </div>
    <div id="chart"></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageScript" runat="server">
    <script type="text/javascript" src="../../Scripts/d3.js"></script>
    <script>
        var w = 960,
            h = 500,
            json = JSON.parse(applicationJSON);

        var colors = {
            application: ["#42FFDF", "#208DCC", "#658699", "#3D9998"],
            server:["#FF9D75","#CC6756"]
        };

        var vis = d3.select("body").append("svg:svg")
    .attr("width", w)
    .attr("height", h);

            var force = self.force = d3.layout.force()
        .nodes(json.nodes)
        .links(json.links)
        .gravity(.05)
        .distance(100)
        .charge(-100)
        .size([w, h])
        .start();

            var link = vis.selectAll("line.link")
        .data(json.links)
        .enter().append("svg:line")
        .attr("class", "link")
        .attr("x1", function (d) { return d.source.x; })
        .attr("y1", function (d) { return d.source.y; })
        .attr("x2", function (d) { return d.target.x; })
        .attr("y2", function (d) { return d.target.y; });

            var node = vis.selectAll("g.node")
        .data(json.nodes)
      .enter().append("svg:g")
        .attr("class", "node")
        .call(force.drag);

            node.append("svg:circle")
        .attr("class", "circle")
        .attr("fill", function (node) {
            console.log(colors[node.type][node.group-1]);
            return colors[node.type][node.group-1];
        })
        .attr("x", "-8px")
        .attr("y", "-8px")
        .attr("r", "10px")
        .attr("width", "16px")
        .attr("height", "16px");

            node.append("svg:text")
        .attr("class", "nodetext")
        .attr("dx", 12)
        .attr("dy", ".35em")
        .text(function (d) { return d.name; });

            node.on("mouseover", function (d) {
                console.log(d.groupname);
            });

            force.on("tick", function () {
                link.attr("x1", function (d) { return d.source.x; })
          .attr("y1", function (d) { return d.source.y; })
          .attr("x2", function (d) { return d.target.x; })
          .attr("y2", function (d) { return d.target.y; });

                node.attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; });
            });
    </script>
</asp:Content>
