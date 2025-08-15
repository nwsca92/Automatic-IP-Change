<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        GroupBox1 = New GroupBox()
        PictureBox1 = New PictureBox()
        ComboBox1 = New ComboBox()
        GroupBox2 = New GroupBox()
        Button3 = New Button()
        Button2 = New Button()
        Button1 = New Button()
        TextBox4 = New TextBox()
        TextBox3 = New TextBox()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        StatusStrip1 = New StatusStrip()
        ToolStripStatusLabel1 = New ToolStripStatusLabel()
        ToolStripStatusLabel2 = New ToolStripStatusLabel()
        ToolStripStatusLabel3 = New ToolStripStatusLabel()
        Timer1 = New Timer(components)
        GroupBox1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        StatusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(PictureBox1)
        GroupBox1.Controls.Add(ComboBox1)
        GroupBox1.Location = New Point(12, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(200, 201)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Interface"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = My.Resources.Resources.yupp_generated_image_697772_removebg_preview
        PictureBox1.Location = New Point(6, 56)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(188, 139)
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
        PictureBox1.TabIndex = 2
        PictureBox1.TabStop = False
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(6, 22)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(188, 23)
        ComboBox1.TabIndex = 1
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(Button3)
        GroupBox2.Controls.Add(Button2)
        GroupBox2.Controls.Add(Button1)
        GroupBox2.Controls.Add(TextBox4)
        GroupBox2.Controls.Add(TextBox3)
        GroupBox2.Controls.Add(TextBox1)
        GroupBox2.Controls.Add(TextBox2)
        GroupBox2.Location = New Point(290, 17)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(352, 196)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        GroupBox2.Text = "Properties"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(128, 151)
        Button3.Name = "Button3"
        Button3.Size = New Size(105, 39)
        Button3.TabIndex = 3
        Button3.Text = "Set DHCP"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(11, 151)
        Button2.Name = "Button2"
        Button2.Size = New Size(111, 39)
        Button2.TabIndex = 2
        Button2.Text = "Set IP Statis"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.BackgroundImageLayout = ImageLayout.Zoom
        Button1.Location = New Point(239, 151)
        Button1.Name = "Button1"
        Button1.Size = New Size(107, 39)
        Button1.TabIndex = 1
        Button1.Text = "Simpan IP"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(11, 109)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(335, 23)
        TextBox4.TabIndex = 0
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(11, 80)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(335, 23)
        TextBox3.TabIndex = 0
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(11, 22)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(335, 23)
        TextBox1.TabIndex = 0
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(11, 51)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(335, 23)
        TextBox2.TabIndex = 0
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.Items.AddRange(New ToolStripItem() {ToolStripStatusLabel1, ToolStripStatusLabel2, ToolStripStatusLabel3})
        StatusStrip1.Location = New Point(0, 277)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(683, 22)
        StatusStrip1.TabIndex = 2
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' ToolStripStatusLabel1
        ' 
        ToolStripStatusLabel1.AutoSize = False
        ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        ToolStripStatusLabel1.Size = New Size(70, 17)
        ToolStripStatusLabel1.Text = "Initializing..."
        ToolStripStatusLabel1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ToolStripStatusLabel2
        ' 
        ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        ToolStripStatusLabel2.Size = New Size(468, 17)
        ToolStripStatusLabel2.Spring = True
        ToolStripStatusLabel2.Text = "(No adapter selected)"
        ' 
        ' ToolStripStatusLabel3
        ' 
        ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        ToolStripStatusLabel3.Size = New Size(99, 17)
        ToolStripStatusLabel3.Text = "--/--/----  --:--:--"
        ToolStripStatusLabel3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 2000
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        ClientSize = New Size(683, 299)
        Controls.Add(StatusStrip1)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MaximumSize = New Size(1000, 1000)
        Name = "Form1"
        SizeGripStyle = SizeGripStyle.Hide
        StartPosition = FormStartPosition.CenterScreen
        Text = "Ganti IP Otomatis"
        GroupBox1.ResumeLayout(False)
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel3 As ToolStripStatusLabel
    Friend WithEvents Timer1 As Timer

End Class
