Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Media
Imports System.Windows.Media.Imaging


''' <summary>
''' 带有感叹号的提示图形
''' </summary>
Public Class NotifyAdorner
        Inherits Adorner
        Private _visuals As VisualCollection
        Private _canvas As Canvas
        Private _image As Image
        Private _toolTip As TextBlock

        Public Sub New(adornedElement As UIElement, errorMessage As String)
            MyBase.New(adornedElement)
            _visuals = New VisualCollection(Me)

        _image = New Image() With {
                .Width = 16,
                .Height = 16,
                .Source = New BitmapImage(New Uri("/warning.png", UriKind.RelativeOrAbsolute))
            }

        _toolTip = New TextBlock() With {
                .Text = errorMessage
            }
        _image.ToolTip = _toolTip

            _canvas = New Canvas()
            _canvas.Children.Add(_image)
            _visuals.Add(_canvas)
        End Sub

        Protected Overrides ReadOnly Property VisualChildrenCount() As Integer
            Get
                Return _visuals.Count
            End Get
        End Property

        Protected Overrides Function GetVisualChild(index As Integer) As Visual
            Return _visuals(index)
        End Function

        Public Sub ChangeToolTip(errorMessage As String)
            _toolTip.Text = errorMessage
        End Sub

        Protected Overrides Function MeasureOverride(constraint As Size) As Size
            Return MyBase.MeasureOverride(constraint)
        End Function

        Protected Overrides Function ArrangeOverride(finalSize As Size) As Size
            _canvas.Arrange(New Rect(finalSize))
            _image.Margin = New Thickness(finalSize.Width + 3, 0, 0, 0)

            Return MyBase.ArrangeOverride(finalSize)
        End Function
    End Class

