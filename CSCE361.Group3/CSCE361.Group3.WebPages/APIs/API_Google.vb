Module API_Google
#Region "Google Map Loading/Plotting"
    'DONE: works
    Public Function populateGoogleMap(ByVal oPictures As DataTable) As String
        Dim sScript As New StringBuilder

        Dim sMarkers As String = getMarkers(oPictures)
        Dim sSetmap As String = getSetMapsForMarkers(oPictures)
        Dim sListeners As String = getSetListenersForMarkers(oPictures)

        sScript.Append("<script type='text/javascript'>")
        sScript.Append("var markerZ = new google.maps.Marker();") 'create blank marker
        sScript.Append(sMarkers)
        sScript.Append("function initialize() { var mapOptions = {center: new google.maps.LatLng(40.82011, -96.700759), zoom: 16, mapTypeId : google.maps.MapTypeId.HYBRID};")
        sScript.Append("var map = new google.maps.Map(document.getElementById('googlemap'), mapOptions);")
        sScript.Append(sSetmap)
        sScript.Append("markerZ.setMap(map);") 'set blank marker to the map
        sScript.Append("google.maps.event.addListener(map, 'click', function(event) {addMarker(event.latLng); });") 'allows marker to be placed on click
        sScript.Append("}")
        sScript.Append("google.maps.event.addDomListener(window, 'load', initialize);")
        sScript.Append(sListeners)
        'function/listner to clear additional user-added markers on the map and set new markers coordinates in textbox for use
        sScript.Append("function addMarker(location) { if(!markerZ) { markerZ = new google.maps.Marker({position: location, map: map}); } else {markerZ.setPosition(location);} document.getElementById('MainContent_tbSelectedPointLatLng').value = location; markerZ.setAnimation(google.maps.Animation.BOUNCE);}")
        sScript.Append("</script>")

        Return sScript.ToString
    End Function

    'DONE: works
    Public Function getMarkers(ByVal oPictures As DataTable) As String
        Dim sMarkers As New StringBuilder

        For i As Integer = 0 To oPictures.Rows.Count - 1 Step 1
            Dim sbMarker As New StringBuilder
            Dim sLatLng As String = oPictures.Rows(i).Item("Latitude").ToString & ", " & oPictures.Rows(i).Item("Longitude").ToString

            'Dim sCaption As String = oPictures.Rows(i).Item("Caption").ToString 'setting caption as title doesnt work due to problems escaping '
            Dim sPhotoID As String = oPictures.Rows(i).Item("PhotoID").ToString

            sbMarker.Append("var marker" & i.ToString & " = new google.maps.Marker({ position: new google.maps.LatLng( " & sLatLng & "), url: '&photoid=" & sPhotoID & "', title:'Click to see photo " & sPhotoID & "', animation: google.maps.Animation.DROP});")

            sMarkers.Append(sbMarker.ToString)
        Next

        Return sMarkers.ToString
    End Function


    'DONE: works
    Public Function getSetMapsForMarkers(ByVal oPictures As DataTable) As String
        Dim sSetMap As New StringBuilder

        For i As Integer = 0 To oPictures.Rows.Count - 1 Step 1
            Dim sbMarker As New StringBuilder

            sbMarker.Append("marker")
            sbMarker.Append(i.ToString)
            sbMarker.Append(".setMap(map);")

            sSetMap.Append(sbMarker.ToString)
        Next

        Return sSetMap.ToString
    End Function

    'DONE: works
    Public Function getSetListenersForMarkers(ByVal oPictures As DataTable) As String
        Dim sListeners As New StringBuilder

        For i As Integer = 0 To oPictures.Rows.Count - 1 Step 1
            Dim sbMarker As New StringBuilder

            'sbMarker.Append("google.maps.event.addListener(marker")
            'sbMarker.Append(i.ToString)
            'sbMarker.Append(",'click', function () { window.location.href = window.document.URL + marker")
            'sbMarker.Append(i.ToString)
            'sbMarker.Append(".url; });")


            'Updated to accomodate loading pictures in a new window
            sbMarker.Append("google.maps.event.addListener(marker")
            sbMarker.Append(i.ToString)
            sbMarker.Append(",'click', function () { window.open('http://54.88.28.177/PictureDetails.aspx?login=1' + marker")
            'sbMarker.Append(",'click', function () { window.open('http://localhost:33627/PictureDetails.aspx?login=1' + marker")
            sbMarker.Append(i.ToString)
            sbMarker.Append(".url); });")


            sListeners.Append(sbMarker.ToString)
        Next

        Return sListeners.ToString
    End Function
#End Region
End Module
